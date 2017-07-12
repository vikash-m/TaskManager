using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDomain.DomainModel
{
    public class TaskAssignmentDm
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime? AssignedOn { get; set; }
        public string ReAssignedBy { get; set; }
        public DateTime? ReAssignedOn { get; set; }

        public virtual Task Task { get; set; }
        public virtual UserDetailDm UserDetail { get; set; }
        public virtual UserDetailDm UserDetail1 { get; set; }
    }
}
