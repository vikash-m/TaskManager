
using System.Collections.Generic;

namespace TaskDomain.DomainModel
{
    public class TaskDetail
    {
        public TaskDm Task { get; set; }
        public List<TaskDocumentDm> TaskDocumentDm { get; set; }
    }
}