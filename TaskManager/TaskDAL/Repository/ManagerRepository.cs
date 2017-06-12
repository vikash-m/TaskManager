using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain.DomainModel;

namespace TaskDAL.Repository
{
    public class ManagerRepository
    {
        TaskManagerEntities _db = new TaskManagerEntities();

        public List<UserdetailDm> GetEmployeesDetailsByManagerId(long? ManagerId)
        {                
                var result = from data in _db.Userdetails
                                    where data.ManagerId == ManagerId
                                    select data;
            var employeeList = new List<UserdetailDm>();
            foreach (var employee in result)
            {
                var list = new UserdetailDm
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
                };
                employeeList.Add(list);
            }

            return employeeList;
        }


        public bool AddTask(TaskDm taskDm)
        {

            var task = new Task
            {
                Title = taskDm.Title,
                CreatedBy = taskDm.CreatedBy,
                AssignedTo = taskDm.AssignedTo,
                StartDate = taskDm.StartDate,
                EndDate = taskDm.EndDate,
                Description = taskDm.Description,
                CreateDate = taskDm.CreateDate            

            };
            var result = _db.Tasks.Add(task);
            _db.SaveChanges();
            return true;
        }

        public bool AddTaskDocument(TaskDocumentDm taskDocument)
        {

            var task = new TaskDocument
            {                
                AddedBy = taskDocument.AddedBy,               
                CreateDate = taskDocument.CreateDate,
                DocumentPath = taskDocument.DocumentPath,
                TaskId = taskDocument.TaskId
            };
            var result = _db.TaskDocuments.Add(task);
            _db.SaveChanges();
            return true;
        }
        public List<TaskDm>  GetAllTask()
        {
            var result = _db.Tasks.ToList();
            var taskList = new List<TaskDm>();
            
            foreach(var task in result)
            {
                var list = new TaskDm
                {
                    Id = (long)task.Id,
                    Title = task.Title,
                    CreatedByName = GetEmployeeNameById(task.CreatedBy),
                    AssignedToName = GetEmployeeNameById(task.AssignedTo),
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    Description = task.Description,
                    CreateDate = task.CreateDate,
                    TaskStatusId = (long)task.TaskStatusId,
                    TaskStatus = GetTaskStatusByTaskStatusId((long)task.TaskStatusId)
                };
                taskList.Add(list);
            }
            return taskList;
        }

        public string GetTaskStatusByTaskStatusId(long taskId)
        {
            var result = from data in _db.TaskStatus
                             where data.Id == taskId
                             select data.Status;

            var taskStatus = result.FirstOrDefault();
            return taskStatus;
        }

        public string GetEmployeeNameById(long id)
        {
            var result = from data in _db.Userdetails
                         where data.Id == id
                         select data.FirstName;
            var firstName = result.FirstOrDefault();
            return firstName;
        } 
        
        public bool UpdateTask(TaskDm task)
        {
            //var result = from data in _db.Tasks
            //                      where data.Id == task.Id
            //                      select data;
            //var taskToBeUpdated = result.FirstOrDefault();
                      
            try
            {
                using (var ctx = new TaskManagerEntities())
                {

                    var taskToBeUpdated = new Task();
                    taskToBeUpdated = ctx.Tasks.FirstOrDefault(i => i.Id == task.Id);
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

        public bool DeleteTask(TaskDm task)
        {
            //var result = from data in _db.Tasks
            //                      where data.Id == task.Id
            //                      select data;
            //var taskToBeUpdated = result.FirstOrDefault();

            try
            {
                using (var ctx = new TaskManagerEntities())
                {

                    var taskToBeUpdated = new Task();
                    taskToBeUpdated = ctx.Tasks.FirstOrDefault(i => i.Id == task.Id);
                    taskToBeUpdated.IsDeleted = task.IsDeleted;
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

        public string GetTaskNameByTaskId(long? id)
        {
            var taskName = from data in _db.Tasks
                           where data.Id == id
                           select data.Title;

            return taskName.FirstOrDefault();
            
        }
    }
}
