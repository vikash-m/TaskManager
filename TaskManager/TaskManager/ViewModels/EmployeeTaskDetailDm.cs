using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskDomain.DomainModel;

namespace TaskManager.ViewModels
{
    public class EmployeeTaskDetailDm
    {
        public List<TaskDm> Task { get; set; }
        public TaskStatusCountDm TaskCount { get; set; }
    }
}