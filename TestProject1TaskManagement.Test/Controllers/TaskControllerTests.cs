using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Services;
using TaskManagement.WebApi.Controllers;
using TaskManagementAPI.Application.Commands;

namespace TestProject1TaskManagement.Test.Controllers;

public class TaskControllerTests
    {
        private readonly TaskController _controller;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateTaskCommand> _validator;
        private readonly TaskService _taskService;
        private readonly ITaskRepository _taskRepository;
        private readonly ICommentRepository _taskCommentRepository;

        public TaskControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _validator = Substitute.For<IValidator<CreateTaskCommand>>();
            _taskService = new TaskService(_taskRepository, _taskCommentRepository); 
            _taskRepository = Substitute.For<ITaskRepository>();
            _controller = new TaskController(_mediator, _validator, _taskService, _taskRepository);
        }

        [Fact]
        public async Task CreateTask_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var command = new CreateTaskCommand { Title = "Task 1"};
            var validationResult = new FluentValidation.Results.ValidationResult
            {
                Errors = { new FluentValidation.Results.ValidationFailure("Title", "Title is required") }
            };

            _validator.ValidateAsync(command).Returns(Task.FromResult(validationResult));

            // Act
            var result = await _controller.CreateTask(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var validationErrors = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<FluentValidation.Results.ValidationFailure>>(badRequestResult.Value);
            Assert.Single(validationErrors);
        }

        [Fact]
        public async Task CreateTask_ShouldReturnCreated_WhenValidationSucceeds()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Title = "Valid Task"
            };

            // Simulate a successful validation by returning an empty ValidationResult (no errors)
            var validationResult = new FluentValidation.Results.ValidationResult();

            // Mock the validation to return success
            _validator.ValidateAsync(command).Returns(Task.FromResult(validationResult));

            // Simulate the mediator call to create the task
            var createdTask = new Tasks { Id = Guid.NewGuid(), Title = "New Task" };
            _mediator.Send(command).Returns(Task.FromResult(createdTask));

            // Simulate adding a task history
            var taskHistory = new TaskHistory
            {
                TaskId = createdTask.Id,
                ChangeDetails = "Tarefa criada",
                ChangeDate = DateTime.UtcNow
            };
            _taskRepository.AddTaskHistoryAsync(taskHistory).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateTask(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetTaskById", createdAtActionResult.ActionName);
            Assert.Equal(createdTask.Id, createdAtActionResult.RouteValues["id"]);
    
            // Verify that the task history was added
            _taskRepository.Received(1).AddTaskHistoryAsync(Arg.Any<TaskHistory>());
        }
       
        [Fact]
        public async Task UpdateTask_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var command = new UpdateTaskCommand { Id = Guid.NewGuid() }; // Ids diferentes

            // Act
            var result = await _controller.UpdateTask(taskId, command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID da tarefa não corresponde ao ID informado.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateTask_ShouldReturnOk_WhenTaskIsUpdated()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var command = new UpdateTaskCommand { Id = taskId };
            var updatedTask = new Tasks { Id = taskId, Title = "Updated Task" };
            _mediator.Send(command).Returns(Task.FromResult(updatedTask));

            // Act
            var result = await _controller.UpdateTask(taskId, command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTask = Assert.IsType<Tasks>(okResult.Value);
            Assert.Equal(taskId, returnedTask.Id);
        }
    }