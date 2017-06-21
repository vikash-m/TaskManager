using System;
using System.Collections.Generic;
using System.Web;


namespace TaskDomain.DomainModel
{
    public class TaskDocumentDm
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public List<HttpPostedFileBase> Document { get; set; }
        public string DocumentPath { get; set; }
        public int AddedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}