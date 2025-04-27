using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Entities;

namespace TaskManagementSystem.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
        Task<TaskEntity?> GetTaskByIdAsync(int id);
        Task<TaskEntity> AddTaskAsync(TaskEntity task);
        Task UpdateTaskAsync(TaskEntity task);
        Task DeleteTaskAsync(int id);
    }
}
