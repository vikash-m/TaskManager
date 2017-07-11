using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDomain.DomainModel.CustomExceptions
{
    public class EmployeeTaskException : Exception
    {
        public EmployeeTaskException() : base() { }

        public EmployeeTaskException(string message) : base(message) { }
    }
}
