using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagementSystem.Controllers;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Services;
using Xunit;

namespace TaskManagementSystem.UnitTests.Controllers
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<TaskController>> _loggerMock;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<TaskController>>();
            _controller = new TaskController(_taskServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithTaskDtos()
        {
            var tasks = new List<TaskEntity>
            {
                new TaskEntity
                {
                    Id = 1,
                    Title = "Test Task",
                    Description = "Description",
                    DueDate = DateTime.Now,
                    IsCompleted = false,
                    ProjectId = 1,
                    Project = new Project
                    {
                        Id = 1,
                        Name = "Test Project",
                        Description = "Proj desc",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(5),
                        UserId = 1,
                        User = new User
                        {
                            Id = 1,
                            Username = "user",
                            Email = "user@example.com",
                            PasswordHash = "hashed",
                            Projects = new List<Project>()
                        },
                        Tasks = new List<TaskEntity>()
                    }
                }
            };

            var taskDtos = new List<TaskDto>
            {
                new TaskDto
                {
                    Id = 1,
                    Title = "Test Task",
                    Description = "Description",
                    DueDate = tasks[0].DueDate,
                    IsCompleted = false,
                    ProjectId = 1,
                    ProjectName = "Test Project"
                }
            };

            _taskServiceMock.Setup(s => s.GetAllTasksAsync()).ReturnsAsync(tasks);
            _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(tasks)).Returns(taskDtos);

            var result = await _controller.GetAll();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(taskDtos);
        }
    }
}
