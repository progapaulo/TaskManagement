using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories;

public interface ITaskRepository
{
    Task<Tasks> GetByIdAsync(Guid id);
    Task<IEnumerable<Tasks>> GetAllByProjectIdAsync(Guid projectId);
    Task<Tasks> AddAsync(Tasks task);
    Task<Tasks> UpdateAsync(Tasks task);
    Task DeleteAsync(Guid id);
}