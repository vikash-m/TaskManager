using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskDomain.DomainModel
{
    public class EnumClass
    {

        public enum Roles { Admin = 1, Manager, Employee };
        public enum Status { Pending = 1, InProgress, Completed };

    }
}