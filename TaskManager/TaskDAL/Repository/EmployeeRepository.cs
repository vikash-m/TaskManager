using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain;

namespace TaskDAL.Repository
{
    public class EmployeeRepository
    {
        TaskManagerEntities taskManagerEntities = new TaskManagerEntities();
        public List<EmployeeModel> GetEmployee()
        {
            var employees = taskManagerEntities.Userdetails.ToList();
            List <EmployeeModel> EmployeeList = new List<EmployeeModel>();
            foreach (var e in employees)
            {
                EmployeeModel em = new EmployeeModel();
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
    }
}
