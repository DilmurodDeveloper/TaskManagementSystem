using AutoMapper;
using Moq;
using TaskManagementSystem.Controllers;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.Controllers
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new TaskController(_taskServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithTasks()
        {
            var tasks = new List<TaskEntity> { new TaskEntity { Id = 1, Title = "Task1" } };
            var taskDtos = new List<TaskDto> { new TaskDto { Id = 1, Title = "Task1" } };

            _taskServiceMock.Setup(s => s.GetAllTasksAsync()).ReturnsAsync(tasks);
            _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(tasks)).Returns(taskDtos);

            var result = await _controller.GetAll();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(taskDtos);
        }
    }
}
