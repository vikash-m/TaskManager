using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class TaskDm
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long CreatedBy { get; set; }
        public long AssignedTo { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Description { get; set; }
        public Nullable<long> TaskStatusId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TaskStatuDm TaskStatu { get; set; }
       
        public virtual ICollection<TaskDocumentDm> TaskDocuments { get; set; }

    }
}