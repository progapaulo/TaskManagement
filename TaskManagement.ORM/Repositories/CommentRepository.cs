using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.ORM.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DefaultContext _context;

    public CommentRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task AddCommentAsync(Comments comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}