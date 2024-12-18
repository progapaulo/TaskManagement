using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Entities;

public class Tasks : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public List<TaskHistory> History { get; set; } = new List<TaskHistory>();
}