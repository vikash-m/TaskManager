using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain.DomainModel;
using TaskDAL.Repository;


namespace TaskServiceLayer
{
    public class ManagerService
    {
        private readonly ManagerRepository _managerRepository = new ManagerRepository();

        public List<UserdetailDm> GetEmployeesDetailsByManagerId(long ManagerId)
        {

            var employeeList = _managerRepository.GetEmployeesDetailsByManagerId(ManagerId);
            return employeeList;
        }

        public bool AddTask(TaskDm task, long loginUserId)
        {
            task.CreateDate = DateTime.Now;
            task.CreatedBy = loginUserId;
            var result = _managerRepository.AddTask(task);
            return result;
        }

        public bool AddTaskDocument(TaskDocumentDm taskDocument, long loginUserId)
        {
            taskDocument.CreateDate = DateTime.Now;
            taskDocument.AddedBy = loginUserId;
            var result = _managerRepository.AddTaskDocument(taskDocument);
            return result;
        }

        public List<TaskDm> GetAllTask()
        {
            var taskList = _managerRepository.GetAllTask();
            return taskList.ToList();
        }

        public bool UpdateTask(TaskDm task)
        {
            task.ModifiedDate = DateTime.Now;
            var taskList = _managerRepository.UpdateTask(task);
            return taskList;
        }

        public bool DeleteTask(long? id)
        {

            var result = _managerRepository.DeleteTask(id);
            return result;
        }

        public string GetTaskNameByTaskId(long? id)
        {
            return _managerRepository.GetTaskNameByTaskId(id);
        }

        public bool DeleteTaskDocument(TaskDocumentDm taskDocument)
        {
            taskDocument.ModifiedDate = DateTime.Now;
            var result = _managerRepository.DeleteTaskDocument(taskDocument);
            return result;
        }

        public TaskDm GetTaskByTaskId(long? id)
        {
            return _managerRepository.GetTaskByTaskId(id);
        }

        public TaskStatusCountDm GetTaskCounts(long id)
        {
            var taskCounts = _managerRepository.GetTaskCounts(id);
            return taskCounts;
        }

        public TaskDetail GetTaskAndTaskDocumentDetailByTaskId(long? id)
        {
            return _managerRepository.GetTaskAndTaskDocumentDetailsByTaskId(id);
        }
    }
}
