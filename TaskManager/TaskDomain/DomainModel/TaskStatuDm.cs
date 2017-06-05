using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskDomain.DomainModel
{
    public class TaskStatuDm
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }       
        public virtual ICollection<TaskDm> Tasks { get; set; }

    }
}