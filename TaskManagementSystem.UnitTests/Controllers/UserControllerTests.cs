using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagementSystem.Controllers;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Services;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace TaskManagementSystem.UnitTests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UserController>>();

            _controller = new UserController(
                _userServiceMock.Object,
                _mapperMock.Object,
                _loggerMock.Object); 
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "user1",
                    Email = "user1@example.com",
                    PasswordHash = "hashedpassword1", 
                    Role = "User",
                    Projects = new List<Project>() 
                },
                new User
                {
                    Id = 2,
                    Username = "user2",
                    Email = "user2@example.com",
                    PasswordHash = "hashedpassword2", 
                    Role = "User",
                    Projects = new List<Project>() 
                }
            };

            var userDtos = new List<UserDto>
            {
                new UserDto { Id = 1, Username = "user1", Email = "user1@example.com", Role = "User" },
                new UserDto { Id = 2, Username = "user2", Email = "user2@example.com", Role = "User" }
            };

            _userServiceMock.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);
            _mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(userDtos);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenUserExists()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "user1",
                Email = "user1@example.com",
                PasswordHash = "hashedpassword1", 
                Role = "User",
                Projects = new List<Project>() 
            };

            var userDto = new UserDto
            {
                Id = 1,
                Username = "user1",
                Email = "user1@example.com",
                Role = "User"
            };

            _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(userDto);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync((User?)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_ShouldReturnCreated_WhenValidData()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                Username = "newuser",
                Email = "newuser@example.com",
                Password = "password123",
                Role = "User"
            };

            var user = new User
            {
                Id = 1,
                Username = "newuser",
                Email = "newuser@example.com",
                PasswordHash = "hashedpassword123", 
                Role = "User",
                Projects = new List<Project>() 
            };

            var userDto = new UserDto { Id = 1, Username = "newuser", Email = "newuser@example.com", Role = "User" };

            _mapperMock.Setup(m => m.Map<User>(createUserDto)).Returns(user);
            _userServiceMock.Setup(s => s.AddUserAsync(user)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = await _controller.Create(createUserDto);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.Value.Should().BeEquivalentTo(userDto);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenInvalidData()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                Username = "",
                Email = "invalidemail",
                Password = "123",
                Role = "User"
            };

            _controller.ModelState.AddModelError("Username", "Username is required");

            // Act
            var result = await _controller.Create(createUserDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenUserUpdated()
        {
            // Arrange
            var updateUserDto = new UpdateUserDto
            {
                Id = 1,
                Username = "updateduser",
                Email = "updateduser@example.com",
                Password = "newpassword",
                Role = "Admin"
            };

            var existingUser = new User
            {
                Id = 1,
                Username = "existinguser",
                Email = "existinguser@example.com",
                PasswordHash = "existingpassword",
                Role = "User",
                Projects = new List<Project>()
            };

            var updatedUserDto = new UserDto
            {
                Id = 1,
                Username = "updateduser",
                Email = "updateduser@example.com",
                Role = "Admin"
            };

            _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(existingUser);
            _userServiceMock.Setup(s => s.UpdateUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map(It.IsAny<UpdateUserDto>(), It.IsAny<User>()))
                       .Callback<UpdateUserDto, User>((src, dest) =>
                       {
                           dest.Username = src.Username;
                           dest.Email = src.Email;
                           dest.PasswordHash = src.Password;
                           dest.Role = src.Role;
                       });

            _mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(updatedUserDto);

            // Act
            var result = await _controller.Update(1, updateUserDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(updatedUserDto);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenUserDeleted()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "user1",
                Email = "user1@example.com",
                PasswordHash = "hashedpassword1", 
                Role = "User",
                Projects = new List<Project>() 
            };

            _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(user);
            _userServiceMock.Setup(s => s.DeleteUserAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync((User?)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
        }
    }
}
