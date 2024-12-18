using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories;

public interface IProjectRepository
{
    Task<Project> GetByIdAsync(Guid id);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project> AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Guid id);
}