using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.DAL
{
    public class RolesRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();

        public string GetRoleNameById(int id) => _taskManagerEntities.Roles.FirstOrDefault(x => x.RoleId == id)?.RoleName;
    }
}