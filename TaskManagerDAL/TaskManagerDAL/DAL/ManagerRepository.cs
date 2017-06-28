using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using TaskDomain.DomainModel;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.DAL
{
    public class ManagerRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();

        public List<UserDetailDm> GetEmployeesDetailsByManagerId(string managerId)
            => _taskManagerEntities.UserDetails.Where(x => x.ManagerId.Equals(managerId) && x.IsDeleted == false).ToList().Select(employee => new UserDetailDm
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Id = employee.Id
            }).ToList();

        public Task CreateTask(Task task)
        {
            var data = _taskManagerEntities.Tasks.Add(task);
            _taskManagerEntities.SaveChanges();
            task.Id = data.Id;
            return task;
        }

        public List<TaskDm> GetAllTask(string managerId)
            => _taskManagerEntities.Tasks.Where(x => x.CreatedBy == managerId && x.IsDeleted == false).ToList().Select(task => new TaskDm
            {
                Id = task.Id,
                Title = task.Title,
                AssignedToName = GetEmployeeNameById(task.AssignedTo),
                CreatedByName = GetEmployeeNameById(task.CreatedBy),
                Description = task.Description,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                TaskStatus = GetTaskStatusByTaskStatusId(task.TaskStatusId),
                CreateDate = task.CreateDate,
                ModifiedDate = task.ModifiedDate
            }).ToList();

        public string GetTaskStatusByTaskStatusId(int? taskId)
            => _taskManagerEntities.TaskStatus.FirstOrDefault(x => x.Id == taskId)?.Status;

        public string GetEmployeeNameById(string id) => _taskManagerEntities.UserDetails.FirstOrDefault(x => x.Id.Equals(id))?.FirstName;

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

        public bool DeleteTask(string id, string loginUser)
        {
            using (var ctx = new TaskManagerEntities())
            {

                var taskToBeUpdated = ctx.Tasks.FirstOrDefault(i => i.Id.Equals(id));
                if (taskToBeUpdated == null)
                {
                    return false;
                }
                taskToBeUpdated.IsDeleted = true;
                taskToBeUpdated.ModifiedDate = DateTime.Now;
                taskToBeUpdated.ModifiedBy = loginUser;
                ctx.SaveChanges();
                return true;
            }
        }

        public string GetTaskNameByTaskId(string id) => _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id.Equals(id))?.Title;

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

        public TaskDm GetTaskByTaskId(string id) => _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id.Equals(id)).IfNotNull(task => new TaskDm
        {
            Id = task.Id,
            Title = task.Title,
            AssignedToName = GetEmployeeNameById(task.AssignedTo),
            AssignedTo = task.AssignedTo,
            CreatedByName = GetEmployeeNameById(task.CreatedBy),
            Description = task.Description,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            TaskStatus = GetTaskStatusByTaskStatusId(task.TaskStatusId),
            CreateDate = task.CreateDate,
            ModifiedDate = task.ModifiedDate
        });

        public bool CheckForTaskName(string title) => _taskManagerEntities.Tasks.FirstOrDefault(x => x.Title.Equals(title) && x.IsDeleted == false) == null;

        public int GetTaskCounts(string employeeId, int statusId) => _taskManagerEntities.Tasks.Count(x => x.TaskStatusId == statusId & x.CreatedBy.Equals(employeeId) && x.IsDeleted == false);

        public List<TaskDocumentDm> GetTaskDocumentBytaskId(string taskId)
            => _taskManagerEntities.TaskDocuments.Where(x => x.TaskId.Equals(taskId)).ToList().Select(taskDocument => new TaskDocumentDm
            {
                Id = taskDocument.Id,
                TaskTitle = GetTaskNameByTaskId(taskDocument.TaskId),
                AddedBy = GetEmployeeNameById(taskDocument.AddedBy),
                CreateDate = taskDocument.CreateDate,
                DocumentPath = taskDocument.DocumentPath,
                ModifiedDate = taskDocument.ModifiedDate

            }).ToList();






    }
}
