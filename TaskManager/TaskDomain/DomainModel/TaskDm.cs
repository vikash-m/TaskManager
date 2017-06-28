using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace TaskDomain.DomainModel
{
    public class TaskDm
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Title For the Task is required")]
        [Remote("CheckForTaskName", "Manager", ErrorMessage = "Task Title already exists. Please enter a different Task Title.")]
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        [Required(ErrorMessage = "Assign To is required")]
        public string AssignedTo { get; set; }
        public string CreatedByName { get; set; }
        public string AssignedToName { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Please provide a description")]
        public string Description { get; set; }
        public int TaskStatusId { get; set; }
        public string TaskStatus { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<HttpPostedFileBase> Document { get; set; }
        public bool IsDeleted { get; set; }
        public virtual TaskStatuDm TaskStatu { get; set; }
        public virtual ICollection<TaskDocumentDm> TaskDocuments { get; set; }

    }
}