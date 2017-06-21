﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.DAL
{
    public class ManagerRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();

        public List<UserDetail> GetEmployeesDetailsByManagerId(int managerId)
            => _taskManagerEntities.UserDetails.Where(x => x.ManagerId == managerId).ToList();

        public Task CreateTask(Task task)
        {
            var data = _taskManagerEntities.Tasks.Add(task);
            _taskManagerEntities.SaveChanges();
            task.Id = data.Id;
            return task;
        }

        public List<Task> GetAllTask(int managerId)
            => _taskManagerEntities.Tasks.Where(x => x.CreatedBy == managerId).ToList();

        public string GetTaskStatusByTaskStatusId(int taskId)
            => _taskManagerEntities.TaskStatus.FirstOrDefault(x => x.Id == taskId)?.Status;

        public string GetEmployeeNameById(int id) => _taskManagerEntities.UserDetails.FirstOrDefault(x => x.Id == id)?.FirstName;

        public bool UpdateTask(Task task)
        {
            using (var ctx = new TaskManagerEntities())
            {
                var taskToBeUpdated = ctx.Tasks.FirstOrDefault(i => i.Id == task.Id);
                if (taskToBeUpdated == null)
                {
                    return false;
                }
                taskToBeUpdated.Title = task.Title;
                taskToBeUpdated.AssignedTo = task.AssignedTo;
                taskToBeUpdated.StartDate = task.StartDate;
                taskToBeUpdated.EndDate = task.EndDate;
                taskToBeUpdated.Description = task.Description;
                taskToBeUpdated.ModifiedDate = task.ModifiedDate;
                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteTask(int id)
        {
            using (var ctx = new TaskManagerEntities())
            {

                var taskToBeUpdated = ctx.Tasks.FirstOrDefault(i => i.Id == id);
                if (taskToBeUpdated == null)
                {
                    return false;
                }
                taskToBeUpdated.IsDeleted = true;
                taskToBeUpdated.ModifiedDate = DateTime.Now;
                ctx.SaveChanges();
                return true;
            }
        }

        public string GetTaskNameByTaskId(int id) => _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id == id)?.Title;

        public bool AddTaskDocument(TaskDocument taskDocument)
        {
            _taskManagerEntities.TaskDocuments.Add(taskDocument);
            _taskManagerEntities.SaveChanges();
            return true;
        }

        public bool DeleteTaskDocument(TaskDocument taskDocument)
        {
            using (var ctx = new TaskManagerEntities())
            {
                var taskToBeUpdated = ctx.TaskDocuments.FirstOrDefault(i => i.Id == taskDocument.Id);
                if (taskToBeUpdated == null)
                {
                    return false;
                }
                taskToBeUpdated.IsDeleted = taskDocument.IsDeleted;
                taskToBeUpdated.ModifiedDate = taskDocument.ModifiedDate;
                ctx.SaveChanges();
                return true;
            }
        }

        public Task GetTaskByTaskId(int id) => _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id == id);

        public bool CheckForTaskName(string title) => _taskManagerEntities.Tasks.FirstOrDefault(x => x.Title.Equals(title)) == null;

        public int GetTaskCounts(int employeeId, int statusId) => _taskManagerEntities.Tasks.Count(x => x.TaskStatusId == statusId & x.AssignedTo == employeeId);






    }
}