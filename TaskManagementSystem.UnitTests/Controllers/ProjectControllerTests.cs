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
    public class ProjectControllerTests
    {
        private readonly Mock<IProjectService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProjectController _controller;

        public ProjectControllerTests()
        {
            _serviceMock = new Mock<IProjectService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new ProjectController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithProjects()
        {
            var projects = new List<Project> { new Project { Id = 1, Name = "Project1" } };
            var projectDtos = new List<ProjectDto> { new ProjectDto { Id = 1, Name = "Project1" } };

            _serviceMock.Setup(s => s.GetAllProjectsAsync()).ReturnsAsync(projects);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProjectDto>>(projects)).Returns(projectDtos);

            var result = await _controller.GetAll();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(projectDtos);
        }
    }
}
