using Moq;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Repositories;
using TaskManagementSystem.Services;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.Services
{
    public class ProjectServiceTests
    {
        private readonly Mock<IRepository<Project>> _repositoryMock;
        private readonly ProjectService _service;

        public ProjectServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Project>>();
            _service = new ProjectService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProjectsAsync_ShouldReturnProjects()
        {
            var projects = new List<Project> { new Project { Id = 1, Name = "ProjectTest" } };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(projects);

            var result = await _service.GetAllProjectsAsync();

            result.Should().BeEquivalentTo(projects);
        }
    }
}
