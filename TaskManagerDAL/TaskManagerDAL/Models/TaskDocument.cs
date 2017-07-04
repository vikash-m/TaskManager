//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManagerDAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskDocument
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string DocumentPath { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual Task Task { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual UserDetail UserDetail1 { get; set; }
    }
}
