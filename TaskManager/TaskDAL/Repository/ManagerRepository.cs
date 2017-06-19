using System;
using System.Collections.Generic;
using System.Linq;
using TaskDomain.DomainModel;


namespace TaskDAL.Repository
{
    public class ManagerRepository
    {
        private readonly TaskManagerEntities _db = new TaskManagerEntities();

        public List<UserdetailDm> GetEmployeesDetailsByManagerId(long? managerId)
            => _db.Userdetails.Where(x => x.ManagerId == managerId).ToList().Select(employee => new UserdetailDm
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                EmailId = employee.EmailId,
                RoleId = employee.RoleId,
                CreateDate = employee.CreateDate,
                ModifiedDate = employee.ModifiedDate,
                IsDeleted = employee.IsDeleted,
                //RoleName = employee.Role,
                ManagerId = employee.ManagerId

            }).ToList();


        public TaskDm AddTask(TaskDm taskDm)
        {

            var task = new Task
            {
                Title = taskDm.Title,
                CreatedBy = taskDm.CreatedBy,
                AssignedTo = taskDm.AssignedTo,
                StartDate = taskDm.StartDate,
                EndDate = taskDm.EndDate,
                Description = taskDm.Description,
                CreateDate = taskDm.CreateDate,
                TaskStatusId = taskDm.TaskStatusId
            };
            var data = _db.Tasks.Add(task);
            _db.SaveChanges();
            taskDm.Id = data.Id;
            return taskDm;
        }

        public List<TaskDm> GetAllTask(long managerId)
            => _db.Tasks.Where(x => x.CreatedBy == managerId).ToList().Select(task => new TaskDm
            {
                Id = task.Id,
                Title = task.Title,
                CreatedByName = GetEmployeeNameById(task.CreatedBy),
                AssignedToName = GetEmployeeNameById(task.AssignedTo),
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Description = task.Description,
                CreateDate = task.CreateDate,
                ModifiedDate = task.ModifiedDate,
                TaskStatusId = task.TaskStatusId,
                TaskStatus = GetTaskStatusByTaskStatusId(task.TaskStatusId)
            }).ToList();


        private string GetTaskStatusByTaskStatusId(long? taskId)
            => _db.TaskStatus.FirstOrDefault(x => x.Id == taskId)?.Status;



        private string GetEmployeeNameById(long id) => _db.Userdetails.FirstOrDefault(x => x.Id == id)?.FirstName;


        public bool UpdateTask(TaskDm task)
        {
            try
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
            catch
            {
                throw;
            }

        }

        public bool DeleteTask(long? id)
        {
            try
            {
                using (var ctx = new TaskManagerEntities())
                {

                    // var taskToBeUpdated = new Task();
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
            catch
            {
                throw;
            }

        }

        public string GetTaskNameByTaskId(long? id) => _db.Tasks.FirstOrDefault(x => x.Id == id)?.Title;


        public bool AddTaskDocument(TaskDocumentDm taskDocument)
        {

            var task = new TaskDocument
            {
                AddedBy = taskDocument.AddedBy,
                CreateDate = taskDocument.CreateDate,
                DocumentPath = taskDocument.DocumentPath,
                TaskId = taskDocument.TaskId
            };
            _db.TaskDocuments.Add(task);
            _db.SaveChanges();
            return true;
        }

        public bool DeleteTaskDocument(TaskDocumentDm taskDocument)
        {
            try
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
            catch
            {
                throw;
            }
        }

        public TaskDm GetTaskByTaskId(long? id) => _db.Tasks.Where(x => x.Id == id).Select(task => new TaskDm()
        {
            Id = task.Id,
            Title = task.Title,
            AssignedTo = task.AssignedTo,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            Description = task.Description,
            CreatedBy = task.CreatedBy,
            CreateDate = task.CreateDate,
            ModifiedDate = task.ModifiedDate,
            IsDeleted = task.IsDeleted,
            TaskStatusId = task.TaskStatusId,
            TaskStatus = GetTaskStatusByTaskStatusId(task.TaskStatusId),
            AssignedToName = GetEmployeeNameById(task.AssignedTo),
            CreatedByName = GetEmployeeNameById(task.CreatedBy)
        }).FirstOrDefault();



        public TaskStatusCountDm GetTaskCounts(long id) => new TaskStatusCountDm
        {
            total = _db.Tasks.Count(x => x.IsDeleted == false & x.CreatedBy == id),
            pending =
                     _db.Tasks.Count(
                         x =>
                             x.TaskStatusId == (long)EnumClass.Status.Pending & x.CreatedBy == id & x.IsDeleted == false),
            inprogress =
                     _db.Tasks.Count(
                         x =>
                             x.TaskStatusId == (long)EnumClass.Status.InProgress & x.CreatedBy == id &
                             x.IsDeleted == false),
            completed =
                     _db.Tasks.Count(
                         x =>
                             x.TaskStatusId == (long)EnumClass.Status.Completed & x.CreatedBy == id &
                             x.IsDeleted == false)
        };




        public bool GetTaskNames(string title)
        {
            return _db.Tasks.FirstOrDefault(x => x.Title.Equals(title)) == null;
        }





    }
}