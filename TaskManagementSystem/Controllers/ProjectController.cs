using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Exceptions;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService projectService, IMapper mapper, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all projects...");
            var projects = await _projectService.GetAllProjectsAsync();
            var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
            _logger.LogInformation("Retrieved {Count} projects.", projectDtos.Count());
            return Ok(projectDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting project by id: {ProjectId}", id);
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                _logger.LogWarning("Project not found with id: {ProjectId}", id);
                throw new CustomException("Loyiha topilmadi", StatusCodes.Status404NotFound);
            }

            var projectDto = _mapper.Map<ProjectDto>(project);
            return Ok(projectDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto createDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid CreateProjectDto model: {@ModelState}", ModelState);
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation("Creating a new project: {@CreateProjectDto}", createDto);
            var projectEntity = _mapper.Map<Project>(createDto);
            var created = await _projectService.AddProjectAsync(projectEntity);
            var projectDto = _mapper.Map<ProjectDto>(created);
            _logger.LogInformation("Project created successfully with id: {ProjectId}", projectDto.Id);
            return CreatedAtAction(nameof(GetById), new { id = projectDto.Id }, projectDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProjectDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid CreateProjectDto model for update: {@ModelState}", ModelState);
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation("Updating project with id: {ProjectId}", id);
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                _logger.LogWarning("Project not found for update with id: {ProjectId}", id);
                throw new CustomException("Loyiha topilmadi", StatusCodes.Status404NotFound);
            }

            _mapper.Map(updateDto, project);
            project.Id = id;
            await _projectService.UpdateProjectAsync(project);

            var projectDto = _mapper.Map<ProjectDto>(project);
            _logger.LogInformation("Project updated successfully: {ProjectId}", projectDto.Id);
            return Ok(projectDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting project with id: {ProjectId}", id);
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                _logger.LogWarning("Project not found for deletion with id: {ProjectId}", id);
                throw new CustomException("Loyiha topilmadi", StatusCodes.Status404NotFound);
            }

            await _projectService.DeleteProjectAsync(id);
            _logger.LogInformation("Project deleted successfully: {ProjectId}", id);
            return NoContent();
        }
    }
}
