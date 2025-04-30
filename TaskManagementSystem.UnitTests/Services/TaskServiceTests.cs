using Moq;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Repositories;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.UnitTests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<IRepository<TaskEntity>> _taskRepositoryMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<IRepository<TaskEntity>>();
            _taskService = new TaskService(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllTasksAsync_ReturnsAllTasks()
        {
            // Arrange
            var tasks = new List<TaskEntity>
    {
        new TaskEntity
        {
            Id = 1,
            Title = "Task 1",
            Description = "Test Task 1",
            DueDate = DateTime.Now,
            ProjectId = 1,
            Project = new Project
            {
                Id = 1,
                Name = "Test Project",
                Description = "Project Description",
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(10),
                UserId = 1,
                User = new User
                {
                    Id = 1,
                    Username = "testuser",
                    Email = "test@example.com",
                    PasswordHash = "hash",
                    Role = "User",
                    Projects = new List<Project>()
                },
                Tasks = new List<TaskEntity>()
            }
        },
        new TaskEntity
        {
            Id = 2,
            Title = "Task 2",
            Description = "Test Task 2",
            DueDate = DateTime.Now,
            ProjectId = 1,
            Project = new Project
            {
                Id = 1,
                Name = "Test Project",
                Description = "Project Description",
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(10),
                UserId = 1,
                User = new User
                {
                    Id = 1,
                    Username = "testuser",
                    Email = "test@example.com",
                    PasswordHash = "hash",
                    Role = "User",
                    Projects = new List<Project>()
                },
                Tasks = new List<TaskEntity>()
            }
        }
    };
            _taskRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllTasksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<List<TaskEntity>>(result); 
            Assert.Equal(2, result.ToList().Count);
        }


        [Fact]
        public async Task GetTaskByIdAsync_ReturnsTask_WhenTaskExists()
        {
            // Arrange
            var task = new TaskEntity
            {
                Id = 1,
                Title = "Task 1",
                Description = "Test Task",
                DueDate = DateTime.Now,
                ProjectId = 1,
                Project = new Project
                {
                    Id = 1,
                    Name = "Test Project",
                    Description = "Project Description",
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(10),
                    UserId = 1,
                    User = new User
                    {
                        Id = 1,
                        Username = "testuser",
                        Email = "test@example.com",
                        PasswordHash = "hash",
                        Role = "User",
                        Projects = new List<Project>()
                    },
                    Tasks = new List<TaskEntity>()
                }
            };
            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(task);

            // Act
            var result = await _taskService.GetTaskByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Task 1", result.Title);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ReturnsNull_WhenTaskDoesNotExist()
        {
            // Arrange
            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((TaskEntity?)null);

            // Act
            var result = await _taskService.GetTaskByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddTaskAsync_ReturnsAddedTask()
        {
            // Arrange
            var task = new TaskEntity
            {
                Title = "New Task",
                Description = "New Task Description",
                DueDate = DateTime.Now,
                ProjectId = 1,
                Project = new Project
                {
                    Id = 1,
                    Name = "Test Project",
                    Description = "Project Description",
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(10),
                    UserId = 1,
                    User = new User
                    {
                        Id = 1,
                        Username = "testuser",
                        Email = "test@example.com",
                        PasswordHash = "hash",
                        Role = "User",
                        Projects = new List<Project>()
                    },
                    Tasks = new List<TaskEntity>()
                }
            };
            _taskRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<TaskEntity>())).ReturnsAsync(task);

            // Act
            var result = await _taskService.AddTaskAsync(task);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Task", result.Title);
            Assert.Equal("New Task Description", result.Description);
        }

        [Fact]
        public async Task UpdateTaskAsync_CallsUpdateOnRepository()
        {
            // Arrange
            var task = new TaskEntity
            {
                Id = 1,
                Title = "Updated Task",
                Description = "Updated Description",
                DueDate = DateTime.Now,
                ProjectId = 1,
                Project = new Project
                {
                    Id = 1,
                    Name = "Test Project",
                    Description = "Project Description",
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(10),
                    UserId = 1,
                    User = new User
                    {
                        Id = 1,
                        Username = "testuser",
                        Email = "test@example.com",
                        PasswordHash = "hash",
                        Role = "User",
                        Projects = new List<Project>()
                    },
                    Tasks = new List<TaskEntity>()
                }
            };
            _taskRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<TaskEntity>())).Returns(Task.CompletedTask);

            // Act
            await _taskService.UpdateTaskAsync(task);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<TaskEntity>()), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_CallsDeleteOnRepository()
        {
            // Arrange
            _taskRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _taskService.DeleteTaskAsync(1);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
