using AutoMapper;
using Moq;
using TaskManagementSystem.Controllers;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Microsoft.Extensions.Logging;

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
            // Arrange
            var projects = new List<Project> { new Project { Id = 1, Name = "Project1" } };
            var projectDtos = new List<ProjectDto> { new ProjectDto { Id = 1, Name = "Project1" } };

            _serviceMock.Setup(s => s.GetAllProjectsAsync()).ReturnsAsync(projects);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProjectDto>>(projects)).Returns(projectDtos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(projectDtos);
        }
    }
}
