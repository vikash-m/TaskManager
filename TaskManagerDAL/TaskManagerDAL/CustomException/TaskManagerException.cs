using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagerDAL.CustomException
{
    public class TaskManagerException:Exception
    {
        public TaskManagerException():base()
        {

        }
        public TaskManagerException(string message) : base(message)
        {

        }
    }
}