using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            var createdProject = await _projectService.AddProjectAsync(project);
            return CreatedAtAction(nameof(GetById), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Project project)
        {
            if (id != project.Id)
                return BadRequest("ID mos emas!");

            await _projectService.UpdateProjectAsync(project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
