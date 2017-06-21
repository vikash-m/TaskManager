using System;
using System.Collections.Generic;

namespace TaskDomain.DomainModel
{
    public class TaskStatuDm
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<TaskDm> Tasks { get; set; }

    }
}