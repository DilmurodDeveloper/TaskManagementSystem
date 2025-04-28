using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
            return Ok(projectDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                throw new CustomException("Loyiha topilmadi", StatusCodes.Status404NotFound);

            var projectDto = _mapper.Map<ProjectDto>(project);
            return Ok(projectDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto createDto)
        {
            if (!ModelState.IsValid)
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);

            var projectEntity = _mapper.Map<Project>(createDto);
            var created = await _projectService.AddProjectAsync(projectEntity);
            var projectDto = _mapper.Map<ProjectDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = projectDto.Id }, projectDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProjectDto updateDto)
        {
            if (!ModelState.IsValid)
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);

            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                throw new CustomException("Loyiha topilmadi", StatusCodes.Status404NotFound);

            _mapper.Map(updateDto, project);
            project.Id = id;
            await _projectService.UpdateProjectAsync(project);

            var projectDto = _mapper.Map<ProjectDto>(project);
            return Ok(projectDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                throw new CustomException("Loyiha topilmadi", StatusCodes.Status404NotFound);

            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
