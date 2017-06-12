using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Enum
{
    public class Enum
    {

        public enum Roles { Admin = 1, Manager, Employee };
        public enum Status { Pending = 1, InProgress, Completed };

    }
}