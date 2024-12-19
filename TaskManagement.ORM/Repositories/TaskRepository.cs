using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.ORM.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DefaultContext _context;

    public TaskRepository(DefaultContext context)
    {
        _context = context;
    }
    public async Task<Tasks> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tasks>> GetAllByProjectIdAsync(Guid projectId)
    {
        return await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task AddTaskHistoryAsync(TaskHistory taskHistory)
    {
        await _context.TaskHistories.AddAsync(taskHistory);
        await _context.SaveChangesAsync();
    }

    public async Task<Tasks> GetTaskByIdAsync(Guid taskId)
    {
        return await _context.Tasks
            .Include(t => t.Comentarios)
            .Include(t => t.History)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task<Tasks> AddAsync(Tasks task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<Tasks> UpdateAsync(Tasks task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}