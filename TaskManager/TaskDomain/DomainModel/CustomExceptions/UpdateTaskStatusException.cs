using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDomain.DomainModel.CustomExceptions
{
    public class UpdateTaskStatusException : Exception
    {
        public UpdateTaskStatusException() : base() { }

        public UpdateTaskStatusException(string message) : base(message) { }
    }
}
