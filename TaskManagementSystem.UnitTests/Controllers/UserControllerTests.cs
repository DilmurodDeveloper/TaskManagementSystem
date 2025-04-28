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
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new UserController(_userServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, Username = "Test" } };
            var userDtos = new List<UserDto> { new UserDto { Id = 1, Username = "Test" } };

            _userServiceMock.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);
            _mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(userDtos);
        }
    }
}
