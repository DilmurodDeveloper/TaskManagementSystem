using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Exceptions;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService, IMapper mapper, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all tasks...");
            var tasks = await _taskService.GetAllTasksAsync();
            var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            _logger.LogInformation("Retrieved {Count} tasks.", taskDtos.Count());
            return Ok(taskDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting task by id: {TaskId}", id);
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                _logger.LogWarning("Task not found with id: {TaskId}", id);
                throw new CustomException("Vazifa topilmadi", StatusCodes.Status404NotFound);
            }

            var taskDto = _mapper.Map<TaskDto>(task);
            return Ok(taskDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto createDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid CreateTaskDto model: {@ModelState}", ModelState);
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation("Creating a new task: {@CreateTaskDto}", createDto);
            var taskEntity = _mapper.Map<TaskEntity>(createDto);
            var created = await _taskService.AddTaskAsync(taskEntity);
            var taskDto = _mapper.Map<TaskDto>(created);
            _logger.LogInformation("Task created successfully with id: {TaskId}", taskDto.Id);
            return CreatedAtAction(nameof(GetById), new { id = taskDto.Id }, taskDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTaskDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid CreateTaskDto model for update: {@ModelState}", ModelState);
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation("Updating task with id: {TaskId}", id);
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                _logger.LogWarning("Task not found for update with id: {TaskId}", id);
                throw new CustomException("Vazifa topilmadi", StatusCodes.Status404NotFound);
            }

            _mapper.Map(updateDto, task);
            task.Id = id;
            await _taskService.UpdateTaskAsync(task);

            var taskDto = _mapper.Map<TaskDto>(task);
            _logger.LogInformation("Task updated successfully: {TaskId}", taskDto.Id);
            return Ok(taskDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting task with id: {TaskId}", id);
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                _logger.LogWarning("Task not found for deletion with id: {TaskId}", id);
                throw new CustomException("Vazifa topilmadi", StatusCodes.Status404NotFound);
            }

            await _taskService.DeleteTaskAsync(id);
            _logger.LogInformation("Task deleted successfully: {TaskId}", id);
            return NoContent();
        }
    }
}
