using Moq;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Repositories;
using TaskManagementSystem.Services;

public class ProjectServiceTests
{
    private readonly Mock<IRepository<Project>> _projectRepositoryMock;
    private readonly ProjectService _projectService;

    public ProjectServiceTests()
    {
        _projectRepositoryMock = new Mock<IRepository<Project>>();
        _projectService = new ProjectService(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllProjectsAsync_ReturnsProjects_WhenProjectsExist()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Username = "user1",
            Email = "user1@example.com",
            PasswordHash = "hashedpassword",
            Role = "User",
            Projects = new List<Project>() 
        };

        var projects = new List<Project>
        {
            new Project
            {
                Id = 1,
                Name = "Project 1",
                Description = "Description 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                UserId = 1,
                User = user, 
                Tasks = new List<TaskEntity>()
            },
            new Project
            {
                Id = 2,
                Name = "Project 2",
                Description = "Description 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(2),
                UserId = 2,
                User = user, 
                Tasks = new List<TaskEntity>()
            }
        };

        _projectRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(projects);

        // Act
        var result = await _projectService.GetAllProjectsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetProjectByIdAsync_ReturnsProject_WhenProjectExists()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Username = "user1",
            Email = "user1@example.com",
            PasswordHash = "hashedpassword",
            Role = "User",
            Projects = new List<Project>() 
        };

        var project = new Project
        {
            Id = 1,
            Name = "Project 1",
            Description = "Description 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            UserId = 1,
            User = user,
            Tasks = new List<TaskEntity>()
        };

        _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(project);

        // Act
        var result = await _projectService.GetProjectByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Project 1", result.Name);
    }

    [Fact]
    public async Task GetProjectByIdAsync_ReturnsNull_WhenProjectDoesNotExist()
    {
        // Arrange
        _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Project?)null);

        // Act
        var result = await _projectService.GetProjectByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddProjectAsync_ReturnsAddedProject()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Username = "user1",
            Email = "user1@example.com",
            PasswordHash = "hashedpassword",
            Role = "User",
            Projects = new List<Project>()
        };

        var project = new Project
        {
            Name = "New Project",
            Description = "New Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            UserId = 1,
            User = user,
            Tasks = new List<TaskEntity>()
        };

        _projectRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Project>())).ReturnsAsync(project);

        // Act
        var result = await _projectService.AddProjectAsync(project);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Project", result.Name);
    }

    [Fact]
    public async Task UpdateProjectAsync_CallsUpdateRepositoryMethod()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Username = "user1",
            Email = "user1@example.com",
            PasswordHash = "hashedpassword",
            Role = "User",
            Projects = new List<Project>() 
        };

        var project = new Project
        {
            Id = 1,
            Name = "Updated Project",
            Description = "Updated Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(1),
            UserId = 1,
            User = user, 
            Tasks = new List<TaskEntity>()
        };

        _projectRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Project>())).Returns(Task.CompletedTask);

        // Act
        await _projectService.UpdateProjectAsync(project);

        // Assert
        _projectRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Project>()), Times.Once);
    }

    [Fact]
    public async Task DeleteProjectAsync_CallsDeleteRepositoryMethod()
    {
        // Arrange
        _projectRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        await _projectService.DeleteProjectAsync(1);

        // Assert
        _projectRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}
