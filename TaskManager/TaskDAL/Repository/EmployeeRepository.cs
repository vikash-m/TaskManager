using System;
using System.Collections.Generic;
using System.Linq;

using TaskDomain.DomainModel;

namespace TaskDAL.Repository
{
    public class EmployeeRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();
        public List<EmployeeModelDm> GetEmployee() => _taskManagerEntities.Userdetails.ToList().Select(e => new EmployeeModelDm
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            PhoneNumber = e.PhoneNumber,
            EmailId = e.EmailId,
            CreateDate = e.CreateDate,
            ModifiedDate = e.ModifiedDate,
            IsDeleted = e.IsDeleted,
            RoleId = e.RoleId
        }).ToList();



        public List<TaskStatuDm> GetStatusList() => _taskManagerEntities.TaskStatus.ToList().Select(st => new TaskStatuDm
        {
            Id = st.Id,
            Status = st.Status
        }).ToList();


        public bool UpdateTaskStatus(long id, long status)
        {
            var task = _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id == id);
            if (task != null) task.TaskStatusId = status;
            else return false;
            _taskManagerEntities.SaveChanges();
            return true;
        }

        public TaskStatusCountDm GetTaskCounts(long id) => new TaskStatusCountDm
        {
            total = _taskManagerEntities.Tasks.Count(x => x.AssignedTo == id),
            pending = _taskManagerEntities.Tasks.Count(x => x.TaskStatusId == (long)EnumClass.Status.Pending & x.AssignedTo == id),
            inprogress = _taskManagerEntities.Tasks.Count(x => x.TaskStatusId == (long)EnumClass.Status.InProgress & x.AssignedTo == id),
            completed = _taskManagerEntities.Tasks.Count(x => x.TaskStatusId == (long)EnumClass.Status.Completed & x.AssignedTo == id)
        };


        public List<TaskDm> GetEmployeeTasks(long id)
        {
            var employeeTasks = _taskManagerEntities.Tasks.Where(x => x.AssignedTo == id).ToList();
            var employeeTaskList = new List<TaskDm>();
            foreach (var et in employeeTasks)
            {
                var emtask = new TaskDm
                {
                    Id = et.Id,
                    Title = et.Title,
                    TaskStatusId = et.TaskStatusId,
                    StartDate = et.StartDate,
                    EndDate = et.EndDate,
                    CreatedBy = et.CreatedBy,
                    AssignedTo = et.AssignedTo,
                    CreateDate = et.CreateDate,
                    ModifiedDate = et.ModifiedDate,
                    IsDeleted = et.IsDeleted,
                    Description = et.Description

                };
                //emtask.TaskStatusId = et.TaskStatusId;


                //var et1 = et;
                var createdBy = _taskManagerEntities.Userdetails.FirstOrDefault(x => x.Id == et.CreatedBy);

                if (createdBy?.LastName != null)
                {
                    emtask.CreatedByName = $"{createdBy.FirstName + " " + createdBy.LastName}";
                    // createdBy.FirstName + " " + createdBy.LastName;
                }


                var assignedTo = _taskManagerEntities.Userdetails.FirstOrDefault(x => x.Id == et.AssignedTo);
                //var assignedToName = assignedTo.FirstName;
                if (assignedTo?.LastName != null)
                {
                    emtask.AssignedToName = $"{assignedTo.FirstName + " " + assignedTo.LastName}";
                }

                var status = _taskManagerEntities.TaskStatus.FirstOrDefault(x => x.Id == et.TaskStatusId);
                if (status != null)
                {
                    var taskStatus = status.Status;
                    emtask.TaskStatus = taskStatus;
                }
                //emtask.AssignedToName = assignedToName;
                employeeTaskList.Add(emtask);
            }
            return employeeTaskList;
        }

        public TaskDm GetTaskDetails(long id)
        {
            try
            {


                var taskResult = _taskManagerEntities.Tasks.FirstOrDefault(m => m.Id == id);
                var assignedtoName = _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == taskResult.AssignedTo)?.FirstName;
                var createdByName = _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == taskResult.CreatedBy)?.FirstName;
                var taskobj = new TaskDm();
                if (taskResult == null) return taskobj;
                {
                    taskobj.Id = taskResult.Id;
                    taskobj.AssignedToName = assignedtoName;
                    taskobj.CreatedByName = createdByName;
                    taskobj.Description = taskResult.Description;
                    taskobj.StartDate = taskResult.StartDate;
                    taskobj.Title = taskResult.Title;
                    taskobj.EndDate = taskResult.EndDate;
                    taskobj.TaskStatus = taskResult.TaskStatu.Status;
                    taskobj.CreateDate = taskResult.CreateDate;
                    taskobj.TaskDocuments = taskResult.TaskDocuments.Select(m => new TaskDocumentDm()
                    {
                        AddedBy = m.AddedBy,
                        Id = m.Id,
                        TaskId = m.TaskId,
                        CreateDate = m.CreateDate,
                        DocumentPath = m.DocumentPath
                    }
                    ).ToList();


                }
                return taskobj;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
