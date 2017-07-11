using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDomain.DomainModel.CustomExceptions
{
    public class DashboardTaskCountException : Exception
    {
        public DashboardTaskCountException() : base()
        {

        }
        public DashboardTaskCountException(string message) : base(message)
        {

        }
    }
}
