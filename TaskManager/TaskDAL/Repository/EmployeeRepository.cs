using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain.DomainModel;

namespace TaskDAL.Repository
{
    public class EmployeeRepository
    {
        TaskManagerEntities taskManagerEntities = new TaskManagerEntities();
        public List<EmployeeModelDm> GetEmployee()
        {
            var employees = taskManagerEntities.Userdetails.ToList();
            List <EmployeeModelDm> EmployeeList = new List<EmployeeModelDm>();
            foreach (var e in employees)
            {
                EmployeeModelDm em = new EmployeeModelDm();
                em.Id = e.Id;
                em.FirstName = e.FirstName;
                em.LastName = e.LastName;
                em.PhoneNumber = e.PhoneNumber;
                em.EmailId = e.EmailId;
                em.CreateDate = e.CreateDate;
                em.ModifiedDate = e.ModifiedDate;
                em.IsDeleted = e.IsDeleted;
                em.RoleId = e.RoleId;

                EmployeeList.Add(em);

            }
            return EmployeeList;
        }


        public List<TaskStatuDm> GetStatusList()
        {
            var statusList = taskManagerEntities.TaskStatus.ToList();
            List<TaskStatuDm> StatusList = new List<TaskStatuDm>();
            foreach (var st in statusList)
            {
                TaskStatuDm ts = new TaskStatuDm();
                ts.Id = st.Id;
                ts.Status = st.Status;
                StatusList.Add(ts);

            }
            return StatusList;
        }

        public bool updatetask(long id, long status)
        {
            var task = taskManagerEntities.Tasks.Where(x => x.Id == id).FirstOrDefault();
            task.TaskStatusId = status;
            taskManagerEntities.SaveChanges();
            return true;
        }

        public TaskStatusCountDm GetTaskCounts(long id)
        {
            long totalTasks = taskManagerEntities.Tasks.Count();
            long pending = taskManagerEntities.Tasks.Where(x => x.TaskStatusId == 1 & x.AssignedTo == id).Count();
            long inprogress = taskManagerEntities.Tasks.Where(x => x.TaskStatusId == 2 & x.AssignedTo == id).Count();
            long completed = taskManagerEntities.Tasks.Where(x => x.TaskStatusId == 3 & x.AssignedTo == id).Count();
            TaskStatusCountDm taskStatusCount = new TaskStatusCountDm();
            taskStatusCount.total = totalTasks;
            taskStatusCount.pending = pending;
            taskStatusCount.inprogress = inprogress;
            taskStatusCount.completed = completed;
            return taskStatusCount;
        }


        public List<TaskDm> GetEmployeeTasks(long id)
        {
            var employeeTasks = taskManagerEntities.Tasks.Where(x=> x.AssignedTo==id).ToList();
            List<TaskDm> EmployeeTaskList = new List<TaskDm>();
            foreach (var et in employeeTasks)
            {
                TaskDm emtask = new TaskDm();
                emtask.Id = et.Id;
                emtask.Title = et.Title;
                emtask.TaskStatusId = et.TaskStatusId;
                emtask.StartDate = et.StartDate;
                emtask.EndDate = et.EndDate;
                emtask.CreatedBy = et.CreatedBy;
                emtask.AssignedTo = et.AssignedTo;
                emtask.CreateDate = et.CreateDate;
                emtask.ModifiedDate = et.ModifiedDate;
                emtask.IsDeleted = et.IsDeleted;
                emtask.Description = et.Description;
                emtask.TaskStatusId = et.TaskStatusId;
               

                var createdBy = taskManagerEntities.Userdetails.Where(x => x.Id == et.CreatedBy).FirstOrDefault();
                string createdByName = createdBy.FirstName;
                if(createdBy.LastName != null)
                {
                    createdByName = createdByName + " " + createdBy.LastName;
                }
                emtask.CreatedByName = createdByName;

                var assignedTo = taskManagerEntities.Userdetails.Where(x => x.Id == et.AssignedTo).FirstOrDefault();
                string assignedToName = assignedTo.FirstName;
                if (assignedTo.LastName != null)
                {
                    assignedToName = assignedToName + " " + assignedTo.LastName;
                }

                string taskStatus = taskManagerEntities.TaskStatus.Where(x => x.Id == et.TaskStatusId).FirstOrDefault().Status;
                emtask.TaskStatus = taskStatus;
                emtask.AssignedToName = assignedToName;
                EmployeeTaskList.Add(emtask);

            }
            return EmployeeTaskList;
        }

        public TaskDm GetTaskDetails(long Id)
        {
            try
            {


                var TaskResult = taskManagerEntities.Tasks.FirstOrDefault(m => m.Id == Id);
                var TaskDocumentResult = taskManagerEntities.TaskDocuments.FirstOrDefault(m => m.TaskId == TaskResult.Id);
                var taskobj = new TaskDm();
                taskobj.Id = TaskResult.Id;

              
                    var assignedto= TaskResult.AssignedTo;
              var assignedtoName=  taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == assignedto).FirstName;
                taskobj.AssignedToName = assignedtoName;
                var createdBy = TaskResult.CreatedBy;
                var createdByName=  taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == createdBy).FirstName;

                taskobj.CreatedByName = createdByName;
                taskobj.Description = TaskResult.Description;
                taskobj.StartDate = TaskResult.StartDate;
                taskobj.Title = TaskResult.Title;
                taskobj.EndDate = TaskResult.EndDate;
                taskobj.TaskDocuments = TaskResult.TaskDocuments.Select(m => new TaskDocumentDm() {
                    AddedBy = TaskDocumentResult.AddedBy,
                    Id = TaskDocumentResult.Id,
                    TaskId = TaskDocumentResult.TaskId,
                    DocumentPath = TaskDocumentResult.DocumentPath
                

                }
                ).ToList();

                return taskobj;
            }
            catch(Exception e )
            {
                return null;
            }
        }

    }
}
