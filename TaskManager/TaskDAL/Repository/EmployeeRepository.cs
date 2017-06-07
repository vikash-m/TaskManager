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

        public TaskStatusCountDm GetTaskCounts()
        {
            long totalTasks = taskManagerEntities.Tasks.Count();
            long pending = taskManagerEntities.Tasks.Where(x => x.TaskStatusId == 1).Count();
            long inprogress = taskManagerEntities.Tasks.Where(x => x.TaskStatusId == 2).Count();
            long completed = taskManagerEntities.Tasks.Where(x => x.TaskStatusId == 3).Count();
            TaskStatusCountDm taskStatusCount = new TaskStatusCountDm();
            taskStatusCount.total = totalTasks;
            taskStatusCount.pending = pending;
            taskStatusCount.inprogress = inprogress;
            taskStatusCount.completed = completed;
            return taskStatusCount;
        }


        public List<TaskDm> GetEmployeeTasks()
        {
            var employeeTasks = taskManagerEntities.Tasks.ToList();
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

    }
}
