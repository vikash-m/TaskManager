using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskDomain.DomainModel
{
    public class TaskDocumentDm
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public string DocumentPath { get; set; }
        public long AddedBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}