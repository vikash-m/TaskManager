using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Enum
{
    public class EnumRoles
    {

        public enum Roles { Admin = 1, Manager, Employee };
        public enum Status { Pending = 1, Progress, Completed };

    }
}