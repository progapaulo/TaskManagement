using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories;

public interface ICommentRepository
{
    Task AddCommentAsync(Comments comment);
    Task SaveAsync();
}