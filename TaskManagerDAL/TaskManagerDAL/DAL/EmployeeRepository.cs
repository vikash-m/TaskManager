using System.Collections.Generic;
using System.Linq;
using TaskManagerDAL.Models;
using TaskDomain.DomainModel;
using TaskManagerDAL.Controllers;

namespace TaskManagerDAL.DAL
{
    public class EmployeeRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();
        public List<UserDetail> GetEmployee() => _taskManagerEntities.UserDetails.ToList();
        ManagerController managerController = new ManagerController();

        public List<TaskStatu> GetStatusList() => _taskManagerEntities.TaskStatus.ToList();
        public bool UpdateTaskStatus(int id, int status)
        {
            var task = _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id == id.ToString());
            if (task != null) task.TaskStatusId = status;
            else return false;
            _taskManagerEntities.SaveChanges();
            return true;
        }

        public int GetTaskCount(string employeeId, int statusId) => _taskManagerEntities.Tasks.Count(x => x.TaskStatusId == statusId & x.AssignedTo.Equals(employeeId));
        public List<Task> GetEmployeeTask(string assignedTo) => _taskManagerEntities.Tasks.Where(x => x.AssignedTo.Equals(assignedTo)).ToList();
        public TaskDm GetTaskDetail(string taskId) {
            var tDocuments = _taskManagerEntities
                .TaskDocuments.Where(m => m.TaskId == taskId)
                .Select(m => new TaskDocumentDm()
                {
                    TaskId = m.TaskId,
                    AddedBy = m.AddedBy,
                    TaskTitle = m.TaskTitle,
                    DocumentPath = m.DocumentPath,
                    ModifiedBy = m.ModifiedBy,
                    CreateDate=m.CreateDate
            

                }).ToList();
            var taskResult = _taskManagerEntities.Tasks.FirstOrDefault(m => m.Id.Equals(taskId));
            var task = new TaskDm
            {
                Id = taskResult.Id,
                Title = taskResult.Title,
                AssignedToName = managerController.GetEmployeeNameById(taskResult.AssignedTo),
                CreatedByName = managerController.GetEmployeeNameById(taskResult.CreatedBy),
                Description = taskResult.Description,
                StartDate = taskResult.StartDate,
                EndDate = taskResult.EndDate,
                TaskStatus = managerController.GetTaskStatusByTaskStatusId(taskResult.TaskStatusId),
                CreateDate = taskResult.CreateDate,
                ModifiedDate = taskResult.ModifiedDate,
                TaskDocuments = tDocuments
            };
            return task;
        }
        public List<TaskDocument> GetTaskDoucments(string taskId) => _taskManagerEntities.TaskDocuments.Where(m => m.TaskId.Equals(taskId)).Select(taskDocument => new TaskDocument {
             Id = taskDocument.Id,
             TaskId = taskDocument.TaskId,
             TaskTitle = taskDocument.TaskTitle,
             DocumentPath = taskDocument.DocumentPath,
             AddedBy = taskDocument.AddedBy,
             ModifiedBy = taskDocument.ModifiedBy,
             CreateDate = taskDocument.CreateDate,
             ModifiedDate = taskDocument.ModifiedDate,
             IsDeleted = taskDocument.IsDeleted
    }).ToList();
    }

}

