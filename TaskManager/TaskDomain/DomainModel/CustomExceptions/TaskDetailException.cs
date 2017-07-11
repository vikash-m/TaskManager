using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDomain.DomainModel.CustomExceptions
{
    public class TaskDetailException : Exception
    {
        public TaskDetailException() : base() { }

        public TaskDetailException(string message) : base(message) { }
    }
}
