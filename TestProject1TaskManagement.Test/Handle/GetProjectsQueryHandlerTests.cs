using System.Collections;
using AutoMapper;
using NSubstitute;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.Handlers;
using TaskManagementAPI.Application.Queries;

namespace TestProject1TaskManagement.Test.Handle;

public class GetProjectsQueryHandlerTests
{
    private readonly IProjectRepository _repository = Substitute.For<IProjectRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    [Fact]
    public async Task Handle_ShouldReturnListOfProjects()
    {
        var handler = new GetProjectByIdQueryHandler(_repository);

        _repository.GetAllAsync().Returns(new List<Project> { new Project { Id = new Guid("a04ff894-b057-4837-9412-0fa6fe341b41"), Name = "Test Project" } });
        _mapper.Map<List<Project>>(Arg.Any<List<Project>>()).Returns(new List<Project> { new Project() { Name = "Test Project" } });

        var result = await handler.Handle(new GetProjectByIdQuery(new Guid("a04ff894-b057-4837-9412-0fa6fe341b41")), CancellationToken.None);
        
        Assert.Null(result);
    }
}