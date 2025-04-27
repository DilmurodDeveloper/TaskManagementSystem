using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskEntity> _taskRepository;

        public TaskService(IRepository<TaskEntity> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync() =>
            await _taskRepository.GetAllAsync();

        public async Task<TaskEntity?> GetTaskByIdAsync(int id) =>
            await _taskRepository.GetByIdAsync(id);

        public async Task<TaskEntity> AddTaskAsync(TaskEntity task) =>
            await _taskRepository.AddAsync(task);

        public async Task UpdateTaskAsync(TaskEntity task) =>
            await _taskRepository.UpdateAsync(task);

        public async Task DeleteTaskAsync(int id) =>
            await _taskRepository.DeleteAsync(id);
    }
}
