using Moq;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Repositories;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "user1", Email = "user1@example.com", PasswordHash = "password1", Role = "User", Projects = new List<Project>() },
                new User { Id = 2, Username = "user2", Email = "user2@example.com", PasswordHash = "password2", Role = "Admin", Projects = new List<Project>() }
            };

            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, Username = "user1", Email = "user1@example.com", PasswordHash = "password1", Role = "User", Projects = new List<Project>() };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result?.Id);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User?)null);

            // Act
            var result = await _userService.GetUserByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_ReturnsUser_WhenUsernameExists()
        {
            // Arrange
            var user = new User { Id = 1, Username = "user1", Email = "user1@example.com", PasswordHash = "password1", Role = "User", Projects = new List<Project>() };
            var users = new List<User> { user };

            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetUserByUsernameAsync("user1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("user1", result?.Username);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_ReturnsNull_WhenUsernameDoesNotExist()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "user1", Email = "user1@example.com", PasswordHash = "password1", Role = "User", Projects = new List<Project>() }
            };

            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetUserByUsernameAsync("nonexistentuser");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddUserAsync_AddsUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "newuser", Email = "newuser@example.com", PasswordHash = "newpassword", Role = "User", Projects = new List<Project>() };

            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.AddUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("newuser", result.Username);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdatesUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "user1", Email = "user1@example.com", PasswordHash = "password1", Role = "User", Projects = new List<Project>() };

            _userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            await _userService.UpdateUserAsync(user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<User>(u => u.Id == user.Id)), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_DeletesUser()
        {
            // Arrange
            var userId = 1;

            _userRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }
    }
}
