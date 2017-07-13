using System;
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
        private readonly ManagerRepository _managerRepository = new ManagerRepository();

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

        public int GetTaskCount(string employeeId, int statusId)
        {
            var taskIds = _taskManagerEntities.TaskAssignments.Where(x => x.AssignedTo.Equals(employeeId)).Select(x => x.TaskId).ToList();
            var count =
                _taskManagerEntities.Tasks.Count(
                    x => taskIds.Contains(x.Id) & x.TaskStatusId == statusId & x.IsDeleted.Value == false);
            return count;
        }

        public List<TaskDm> GetEmployeeTask(string assignedTo)
        {
            var taskIds = _taskManagerEntities.TaskAssignments.Where(x => x.AssignedTo.Equals(assignedTo)).Select(x => x.TaskId).ToList();
            var tasks = _taskManagerEntities.Tasks.Where(x => taskIds.Contains(x.Id)).ToList().Select(task => new TaskDm
            {
                Id = task.Id,
                Title = task.Title,
                //AssignedToName = managerRepository.GetEmployeeNameById(task.AssignedTo),
                // AssignedTo = task.AssignedTo,
                CreatedByName = _managerRepository.GetEmployeeNameById(task.CreatedBy),
                Description = task.Description,
                StartDate = Convert.ToDateTime(task.StartDate),
                EndDate = Convert.ToDateTime(task.EndDate),
                TaskStatus = _managerRepository.GetTaskStatusByTaskStatusId(task.TaskStatusId),
                TaskStatusId = task.TaskStatusId,
                CreateDate = Convert.ToDateTime(task.CreateDate),
                ModifiedDate = task.ModifiedDate
            }).ToList();
            return tasks;
        }

        public TaskDm GetTaskDetail(string taskId)
        {
            var taskDetail = _taskManagerEntities.Tasks.FirstOrDefault(m => m.Id.Equals(taskId)).IfNotNull(task => new TaskDm
            {
                Id = task.Id,
                Title = task.Title,
                // AssignedToName = managerRepository.GetEmployeeNameById(task.AssignedTo),
                AssignedTo = _managerRepository.GetAssignToByTaskId(taskId),
                CreatedByName = _managerRepository.GetEmployeeNameById(task.CreatedBy),
                Description = task.Description,
                StartDate = Convert.ToDateTime(task.StartDate),
                EndDate = Convert.ToDateTime(task.EndDate),
                TaskStatus = _managerRepository.GetTaskStatusByTaskStatusId(task.TaskStatusId),
                TaskStatusId = task.TaskStatusId,
                CreateDate = Convert.ToDateTime(task.CreateDate),
                ModifiedDate = task.ModifiedDate,
                TaskDocuments = GetTaskDocumants(task.Id)

            });

            taskDetail.AssignedToName = _managerRepository.GetAssignToNameByAssisnTo(taskDetail.AssignedTo);
            return taskDetail;

        }

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

