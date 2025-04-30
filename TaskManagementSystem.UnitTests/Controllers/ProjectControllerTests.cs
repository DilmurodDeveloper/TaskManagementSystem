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
    public class ProjectControllerTests
    {
        private readonly Mock<IProjectService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<ProjectController>> _loggerMock;
        private readonly ProjectController _controller;

        public ProjectControllerTests()
        {
            _serviceMock = new Mock<IProjectService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<ProjectController>>();
            _controller = new ProjectController(_serviceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithProjects()
        {
            var projects = new List<Project>
            {
               new Project
               {
                   Id = 1,
                   Name = "Project1",
                   Description = "Test project",
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now.AddDays(10),
                   UserId = 1,
                   
                   User = new User
                   {
                       Id = 1,
                       Username = "testuser",
                       Email = "test@example.com",
                       PasswordHash = "hashedpassword",
                       Projects = new List<Project>() 
                   },
                   Tasks = new List<TaskEntity>()
               }
            };

            var projectDtos = new List<ProjectDto>
            {
                new ProjectDto
                {
                    Id = 1,
                    Name = "Project1",
                    Description = "Test project",
                    StartDate = projects[0].StartDate,
                    EndDate = projects[0].EndDate,
                    UserId = 1,
                    UserName = "testuser"
                }
            };

            _serviceMock.Setup(s => s.GetAllProjectsAsync()).ReturnsAsync(projects);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProjectDto>>(projects)).Returns(projectDtos);

            var result = await _controller.GetAll();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(projectDtos);
        }
    }
}
