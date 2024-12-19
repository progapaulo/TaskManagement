using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories;

public interface ITaskRepository
{
    Task<Tasks> GetByIdAsync(Guid id);
    Task<IEnumerable<Tasks>> GetAllByProjectIdAsync(Guid projectId);
    Task AddTaskHistoryAsync(TaskHistory taskHistory);
    
    Task<Tasks> GetTaskByIdAsync(Guid taskId);
    Task<Tasks> AddAsync(Tasks task);
    Task<Tasks> UpdateAsync(Tasks task);
    Task DeleteAsync(Guid id);
}