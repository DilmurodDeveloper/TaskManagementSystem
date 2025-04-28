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
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return Ok(taskDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                throw new CustomException("Vazifa topilmadi", StatusCodes.Status404NotFound);

            var taskDto = _mapper.Map<TaskDto>(task);
            return Ok(taskDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto createDto)
        {
            if (!ModelState.IsValid)
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);

            var taskEntity = _mapper.Map<TaskEntity>(createDto);
            var created = await _taskService.AddTaskAsync(taskEntity);
            var taskDto = _mapper.Map<TaskDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = taskDto.Id }, taskDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTaskDto updateDto)
        {
            if (!ModelState.IsValid)
                throw new CustomException("Ma'lumotlar to‘g‘ri emas", StatusCodes.Status400BadRequest);

            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                throw new CustomException("Vazifa topilmadi", StatusCodes.Status404NotFound);

            _mapper.Map(updateDto, task);
            task.Id = id;
            await _taskService.UpdateTaskAsync(task);

            var taskDto = _mapper.Map<TaskDto>(task);
            return Ok(taskDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                throw new CustomException("Vazifa topilmadi", StatusCodes.Status404NotFound);

            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
