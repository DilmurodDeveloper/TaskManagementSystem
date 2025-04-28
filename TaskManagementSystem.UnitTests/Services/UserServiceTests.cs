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
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _repositoryMock;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _repositoryMock = new Mock<IRepository<User>>();
            _service = new UserService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            var users = new List<User> { new User { Id = 1, Username = "TestUser" } };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            var result = await _service.GetAllUsersAsync();

            result.Should().BeEquivalentTo(users);
        }
    }
}
