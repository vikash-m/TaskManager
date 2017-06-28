using System.Collections.Generic;
using System.Linq;
using TaskManagerDAL.Models;


namespace TaskManagerDAL.DAL
{
    public class EmployeeRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();
        public List<UserDetail> GetEmployee() => _taskManagerEntities.UserDetails.ToList();

        public List<TaskStatu> GetStatusList() => _taskManagerEntities.TaskStatus.ToList();
        public bool UpdateTaskStatus(int id, int status)
        {
            var task = _taskManagerEntities.Tasks.FirstOrDefault(x => x.Id == id.ToString());
            if (task != null) task.TaskStatusId = status;
            else return false;
            _taskManagerEntities.SaveChanges();
            return true;
        }

        public int GetTaskCount(string employeeId, int statusId) => _taskManagerEntities.Tasks.Count(x => x.TaskStatusId == statusId & x.AssignedTo.Equals(employeeId));
        public List<Task> GetEmployeeTask(string assignedTo) => _taskManagerEntities.Tasks.Where(x => x.AssignedTo.Equals(assignedTo)).ToList();
        public Task GetTaskDetail(string taskId) => _taskManagerEntities.Tasks.FirstOrDefault(m => m.Id.Equals(taskId));
    }

}

