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
    public class TaskServiceTests
    {
        private readonly Mock<IRepository<TaskEntity>> _repositoryMock;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _repositoryMock = new Mock<IRepository<TaskEntity>>();
            _service = new TaskService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllTasksAsync_ShouldReturnTasks()
        {
            var tasks = new List<TaskEntity> { new TaskEntity { Id = 1, Title = "TaskTest" } };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(tasks);

            var result = await _service.GetAllTasksAsync();

            result.Should().BeEquivalentTo(tasks);
        }
    }
}
