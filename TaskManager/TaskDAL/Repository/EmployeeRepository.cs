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

                EmployeeTaskList.Add(emtask);

            }
            return EmployeeTaskList;
        }

    }
}
