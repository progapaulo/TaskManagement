using System.Collections;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TaskManagement.Domain.Entities;
using TaskManagement.WebApi.Controllers;
using TaskManagementAPI.Application.Commands;
using TaskManagementAPI.Application.Queries;

namespace TestProject1TaskManagement.Test.Controllers;

public class ProjectControllerTests
{
    private readonly ProjectController _controller;
    private readonly IMediator _mediator;

    public ProjectControllerTests()
    {
        // Substituindo o Mediator com NSubstitute
        _mediator = Substitute.For<IMediator>();
        _controller = new ProjectController(_mediator);
    }

    [Fact]
    public async Task GetAllProjects_ShouldReturnOk_WhenProjectsExist()
    {
        // Arrange
        var projects = new List<Project> { new Project { Id = Guid.NewGuid(), Name = "Project 1" } };
        _mediator.Send(Arg.Is<GetAllProjectsQuery>(q => q != null)).Returns(Task.FromResult(projects.AsEnumerable()));

        // Act
        var result = await _controller.GetAllProjects();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnProjects = Assert.IsAssignableFrom<List<Project>>(okResult.Value);
        Assert.NotEmpty(returnProjects);
    }

    [Fact]
    public async Task GetProjectById_ShouldReturnNotFound_WhenProjectDoesNotExist()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        _mediator.Send(Arg.Is<GetProjectByIdQuery>(q => q.Id == projectId)).Returns(Task.FromResult<Project>(null));

        // Act
        var result = await _controller.GetProjectById(projectId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]            
    public async Task GetProjectById_ShouldReturnOk_WhenProjectExists()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Project { Id = projectId, Name = "Project 1" };
        _mediator.Send(Arg.Is<GetProjectByIdQuery>(q => q.Id == projectId)).Returns(Task.FromResult(project));

        // Act
        var result = await _controller.GetProjectById(projectId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnProject = Assert.IsAssignableFrom<Project>(okResult.Value);
        Assert.Equal(projectId, returnProject.Id);
    }
}