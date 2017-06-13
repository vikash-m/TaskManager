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
        public ManagerRepository ManagerRepository = new ManagerRepository();

        public List<UserdetailDm> GetEmployeesDetailsByManagerId(long ManagerId)
        {
           var employeeList = ManagerRepository.GetEmployeesDetailsByManagerId(ManagerId);                
            return employeeList;
        }

        public bool AddTask(TaskDm task, long loginUserId)
        {
            task.CreateDate = DateTime.Now;               
            task.CreatedBy = loginUserId;
            var result = ManagerRepository.AddTask(task);
            return result;
        }

        public bool AddTaskDocument(TaskDocumentDm taskDocument, long loginUserId)
        {
            taskDocument.CreateDate = DateTime.Now;
            taskDocument.AddedBy = loginUserId;            
            var result = ManagerRepository.AddTaskDocument(taskDocument);
            return result;
        }

        public List<TaskDm> GetAllTask()
        {
            var taskList = ManagerRepository.GetAllTask();
            return taskList;
        }

        public bool UpdateTask(TaskDm task)
        {
            task.ModifiedDate = DateTime.Now;
            var taskList = ManagerRepository.UpdateTask(task);
            return taskList;
        }

        public bool DeleteTask(TaskDm task)
        {
            task.ModifiedDate = DateTime.Now;
            var result = ManagerRepository.DeleteTask(task);
            return result;
        }

        public string GetTaskNameByTaskId(long? id)
        {
           return ManagerRepository.GetTaskNameByTaskId(id);
        }

        public bool DeleteTaskDocument(TaskDocumentDm taskDocument)
        {
            taskDocument.ModifiedDate = DateTime.Now;
            var result = ManagerRepository.DeleteTaskDocument(taskDocument);
            return result;
        }

        public TaskDm GetTaskByTaskId(long? id)
        {
            return ManagerRepository.GetTaskByTaskId(id);
        }
    }
}
