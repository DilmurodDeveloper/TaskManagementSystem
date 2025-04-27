using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;

        public ProjectService(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync() =>
            await _projectRepository.GetAllAsync();

        public async Task<Project?> GetProjectByIdAsync(int id) =>
            await _projectRepository.GetByIdAsync(id);

        public async Task<Project> AddProjectAsync(Project project) =>
            await _projectRepository.AddAsync(project);

        public async Task UpdateProjectAsync(Project project) =>
            await _projectRepository.UpdateAsync(project);

        public async Task DeleteProjectAsync(int id) =>
            await _projectRepository.DeleteAsync(id);
    }
}
