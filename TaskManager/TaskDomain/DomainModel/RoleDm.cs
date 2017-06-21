using System;


namespace TaskDomain.DomainModel
{
    public class RoleDm
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
