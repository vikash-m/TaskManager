
namespace TaskDomain.DomainModel
{
    public class TaskStatusCountDm
    {
        public int Pending { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }
        public int Total { get; set; }
    }
}
