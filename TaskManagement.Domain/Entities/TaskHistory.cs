using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Entities;

public class TaskHistory : BaseEntity
{
    public string ChangeDetails { get; set; }
    public DateTime ChangeDate { get; set; }
    public Guid TaskId { get; set; }
    public Tasks Tasks { get; set; }
}