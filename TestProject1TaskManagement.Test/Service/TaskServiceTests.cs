using FluentAssertions;
using NSubstitute;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Services;

namespace TestProject1TaskManagement.Test.Service;

public class TaskServiceTests
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        // Usando NSubstitute para criar mocks dos repositórios
        _taskRepository = Substitute.For<ITaskRepository>();
        _commentRepository = Substitute.For<ICommentRepository>();

        // Inicializando o serviço
        _taskService = new TaskService(_taskRepository, _commentRepository);
    }

    [Fact]
    public async Task AddCommentToTask_ShouldAddComment_WhenTaskExists()
    {
        // Arrange
        var taskId = new Guid("814403bd-23f3-4b23-be4e-766d730a1130");
        var commentText = "New comment";
        var task = new Tasks { Id = taskId, Title = "Test Task" };

        // Mock the task repository to return an existing task
        _taskRepository.GetTaskByIdAsync(taskId).Returns(task);

        // Act
        await _taskService.AddCommentToTaskAsync(taskId, commentText);
    }
    
    [Fact]
    public void AddCommentToTask_ShouldThrowException_WhenTaskDoesNotExist()
    {
        // Arrange
        var taskId = new Guid("814403bd-23f3-4b23-be4e-766d730a1130");
        var commentText = "New comment";

        // Mockando o repositório para retornar null (tarefa não encontrada)
        _taskRepository.GetTaskByIdAsync(taskId).Returns((Task)null);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(() => _taskService.AddCommentToTaskAsync(taskId, commentText));
    }
}