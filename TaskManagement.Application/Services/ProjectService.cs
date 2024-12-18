using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.DTOs;

namespace TaskManagementAPI.Application.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project> CreateProjectAsync(CreateProjectDto dto);
    Task<Project> GetProjectByIdAsync(Guid id);
    Task UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(Guid id);
}

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    
    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<Project> CreateProjectAsync(CreateProjectDto dto)
    {
        var project = new Project
        {
            Name = dto.Name
        };

        return await _projectRepository.AddAsync(project);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllAsync();
    }

    public async Task<Project> GetProjectByIdAsync(Guid id)
    {
        return await _projectRepository.GetByIdAsync(id);
    }

    public async Task UpdateProjectAsync(Project project)
    {
        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteProjectAsync(Guid id)
    {
        await _projectRepository.DeleteAsync(id);
    }
}