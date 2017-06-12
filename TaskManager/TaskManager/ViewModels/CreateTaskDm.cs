using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskDomain.DomainModel;

namespace TaskManager.ViewModels
{
    public class CreateTaskDm
    {
        public TaskDm Task { get; set; }
        public TaskDocumentDm TaskDocumentDm { get; set; }
    }
}