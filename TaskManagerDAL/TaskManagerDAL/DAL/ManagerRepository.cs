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
            =>
                _taskManagerEntities.UserDetails.Where(x => x.ManagerId.Equals(managerId) && x.IsDeleted == false)
                    .ToList()
                    .Select(userDetailDm => new UserDetailDm
                    {
                        Id = userDetailDm.Id,
                        FirstName = userDetailDm.FirstName,
                        LastName = userDetailDm.LastName,
                        EmailId = userDetailDm.EmailId,
                        PhoneNumber = userDetailDm.PhoneNumber,
                        RoleId = userDetailDm.Role.RoleId,
                        ManagerId = userDetailDm.ManagerId,
                    }).ToList();

        public Task CreateTask(Task task)
        {
            var data = _taskManagerEntities.Tasks.Add(task);
            _taskManagerEntities.SaveChanges();
            task.Id = data.Id;
            return task;
        }

        public List<TaskDm> GetAllTask(string managerId)
        {
            var tasks =
                _taskManagerEntities.Tasks.Where(x => x.CreatedBy == managerId && x.IsDeleted == false)
                    .ToList()
                    .Select(task => new TaskDm
                    {
                        Id = task.Id,
                        Title = task.Title,
                        AssignedTo = GetAssignToByTaskId(task.Id),
                        CreatedByName = GetEmployeeNameById(task.CreatedBy),
                        Description = task.Description,
                        StartDate = Convert.ToDateTime(task.StartDate),
                        EndDate = Convert.ToDateTime(task.EndDate),
                        TaskStatus = GetTaskStatusByTaskStatusId(task.TaskStatusId),
                        CreateDate = Convert.ToDateTime(task.CreateDate),
                        ModifiedDate = task.ModifiedDate
                    }).ToList();
            foreach (var task in tasks)
            {
                var assignTo = GetAssignToNameByAssisnTo(task.AssignedTo);
                task.AssignedToName = assignTo;
            }
            return tasks;
        }

        public string GetTaskStatusByTaskStatusId(int? taskId)
            => _taskManagerEntities.TaskStatus.FirstOrDefault(x => x.Id == taskId)?.Status;

        public string GetEmployeeNameById(string id)
            => _taskManagerEntities.UserDetails.FirstOrDefault(x => x.Id.Equals(id))?.FirstName;

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
                //taskToBeUpdated.AssignedTo = task.AssignedTo;
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

        public string GetTaskNameByTaskId(string id)
            => _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id.Equals(id))?.Title;

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

        public TaskDm GetTaskByTaskId(string id)
        {
            var task = _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id.Equals(id)).IfNotNull(x => new TaskDm
            {
                Id = x.Id,
                Title = x.Title,
                AssignedTo = GetAssignToByTaskId(id),
                CreatedByName = GetEmployeeNameById(x.CreatedBy),
                Description = x.Description,
                StartDate = Convert.ToDateTime(x.StartDate),
                EndDate = Convert.ToDateTime(x.EndDate),
                TaskStatus = GetTaskStatusByTaskStatusId(x.TaskStatusId),
                CreateDate = Convert.ToDateTime(x.CreateDate),
                ModifiedDate = x.ModifiedDate
            });
            task.AssignedToName = GetAssignToNameByAssisnTo(task.AssignedTo);
            return task;
        }

        public bool CheckForTaskName(string title)
            => _taskManagerEntities.Tasks.FirstOrDefault(x => x.Title.Equals(title) && x.IsDeleted == false) == null;

        public int GetTaskCounts(string employeeId, int statusId)
            =>
                _taskManagerEntities.Tasks.Count(
                    x => x.TaskStatusId == statusId & x.CreatedBy.Equals(employeeId) && x.IsDeleted == false);

        public List<TaskDocumentDm> GetTaskDocumentBytaskId(string taskId)
            =>
                _taskManagerEntities.TaskDocuments.Where(x => x.TaskId.Equals(taskId))
                    .ToList()
                    .Select(taskDocument => new TaskDocumentDm
                    {
                        Id = taskDocument.Id,
                        TaskTitle = GetTaskNameByTaskId(taskDocument.TaskId),
                        AddedBy = GetEmployeeNameById(taskDocument.AddedBy),
                        CreateDate = taskDocument.CreateDate,
                        DocumentPath = taskDocument.DocumentPath,
                        ModifiedDate = taskDocument.ModifiedDate

                    }).ToList();


        public bool AddAssignTo(List<TaskAssignment> taskAssignment)
        {
            foreach (var assignTo in taskAssignment)
            {
                _taskManagerEntities.TaskAssignments.Add(assignTo);
            }

            return _taskManagerEntities.SaveChanges() > 0;

        }

        public List<string> GetAssignToByTaskId(string taskId)
        {
            var result =
                _taskManagerEntities.TaskAssignments.Where(x => x.TaskId.Equals(taskId))
                    .Select(x => x.AssignedTo)
                    .ToList();
            return result;
        }

        public List<string> GetAssignToNameByAssisnTo(List<string> ids)
        {
            var assignToName = new List<string>();
            foreach (var id in ids)
            {
                var assignTo = _taskManagerEntities.UserDetails.Where(x => x.Id.Equals(id))
                    .Select(x => x.FirstName).FirstOrDefault();
                assignToName.Add(assignTo);
            }



            return assignToName;
        }

        public List<TaskDm> GetTaskDetails(string managerId, string employeeId)
        {
            var taskIds =
                _taskManagerEntities.TaskAssignments.Where(x => x.AssignedTo.Equals(employeeId))
                    .ToList()
                    .Select(x => x.TaskId)
                    .ToList();
            var tasks = _taskManagerEntities.Tasks.Where(x => taskIds.Contains(x.Id) && x.CreatedBy.Equals(managerId) && x.IsDeleted == false).ToList().Select(x => new TaskDm
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                StartDate = Convert.ToDateTime(x.StartDate),
                EndDate = Convert.ToDateTime(x.EndDate),
                TaskStatus = GetTaskStatusByTaskStatusId(x.TaskStatusId),
                CreateDate = Convert.ToDateTime(x.CreateDate),
                ModifiedDate = x.ModifiedDate
            }).ToList();
            foreach (var task in tasks)
            {
                task.TaskDocuments = GetTaskDocumentBytaskId(task.Id);
            }
            return tasks;
        }

        public int GetEmployeeTaskCount(string managerId, string employeeId, int statusId)
        {
            var taskIds =
               _taskManagerEntities.TaskAssignments.Where(x => x.AssignedTo.Equals(employeeId))
                   .ToList()
                   .Select(x => x.TaskId)
                   .ToList();
            var taskCount =
                _taskManagerEntities.Tasks.Count(x => taskIds.Contains(x.Id) && x.CreatedBy.Equals(managerId) && x.TaskStatusId == statusId && x.IsDeleted == false);
            return taskCount;

        }
    }
}
