using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using TaskDomain.DomainModel;
using TaskManagerDAL.Models;


namespace TaskManagerDAL.DAL
{
    public class EmployeeRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();
        public List<UserDetail> GetEmployee() => _taskManagerEntities.UserDetails.ToList();
        ManagerRepository managerRepository = new ManagerRepository();

        public List<TaskStatuDm> GetStatusList() => _taskManagerEntities.TaskStatus.ToList().Select(taskStatus => new TaskStatuDm
        {
            Id = taskStatus.Id,
            Status = taskStatus.Status
        }).ToList();
        public bool UpdateTaskStatus(string id, int status)
        {
            var task = _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id == id.ToString());
            if (task != null) task.TaskStatusId = status;
            else return false;
            _taskManagerEntities.SaveChanges();
            return true;
        }

        public int GetTaskCount(string employeeId, int statusId) => _taskManagerEntities.Tasks.Count(x => x.TaskStatusId == statusId & x.AssignedTo.Equals(employeeId));
        public List<TaskDm> GetEmployeeTask(string assignedTo) => _taskManagerEntities.Tasks.Where(x => x.AssignedTo.Equals(assignedTo)).ToList().Select(task => new TaskDm
        {
            Id = task.Id,
            Title = task.Title,
            AssignedToName = managerRepository.GetEmployeeNameById(task.AssignedTo),
            AssignedTo = task.AssignedTo,
            CreatedByName = managerRepository.GetEmployeeNameById(task.CreatedBy),
            Description = task.Description,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            TaskStatus = managerRepository.GetTaskStatusByTaskStatusId(task.TaskStatusId),
            TaskStatusId = task.TaskStatusId,
            CreateDate = task.CreateDate,
            ModifiedDate = task.ModifiedDate
        }).ToList();

        public TaskDm GetTaskDetail(string taskId) => _taskManagerEntities.Tasks.FirstOrDefault(m => m.Id.Equals(taskId)).IfNotNull(task => new TaskDm
        {
            Id = task.Id,
            Title = task.Title,
            AssignedToName = managerRepository.GetEmployeeNameById(task.AssignedTo),
            AssignedTo = task.AssignedTo,
            CreatedByName = managerRepository.GetEmployeeNameById(task.CreatedBy),
            Description = task.Description,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            TaskStatus = managerRepository.GetTaskStatusByTaskStatusId(task.TaskStatusId),
            TaskStatusId = task.TaskStatusId,
            CreateDate = task.CreateDate,
            ModifiedDate = task.ModifiedDate,
            TaskDocuments = GetTaskDocumants(task.Id)

        });

        public List<TaskDocumentDm> GetTaskDocumants(string taskId) => _taskManagerEntities.TaskDocuments.Where(m => m.TaskId.Equals(taskId)).Select(taskDocument => new TaskDocumentDm
        {
            Id = taskDocument.Id,
            TaskId = taskDocument.TaskId,
            TaskTitle = taskDocument.TaskTitle,
            DocumentPath = taskDocument.DocumentPath,
            AddedBy = taskDocument.AddedBy,
            ModifiedBy = taskDocument.ModifiedBy,
            CreateDate = taskDocument.CreateDate,
            ModifiedDate = taskDocument.ModifiedDate,
        }).ToList();
    }

    
}

