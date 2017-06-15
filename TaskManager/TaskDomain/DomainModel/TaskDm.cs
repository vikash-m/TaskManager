using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskDomain.DomainModel
{
    public class TaskDm
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long CreatedBy { get; set; }
        public long AssignedTo { get; set; }
        public string CreatedByName { get; set; }
        public string AssignedToName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public long? TaskStatusId { get; set; }
        public string TaskStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<HttpPostedFileBase> Document { get; set; }
        public bool IsDeleted { get; set; }
        public virtual TaskStatuDm TaskStatu { get; set; }
        public virtual ICollection<TaskDocumentDm> TaskDocuments { get; set; }

    }
}