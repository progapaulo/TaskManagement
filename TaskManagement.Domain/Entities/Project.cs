using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; }
    public List<Tasks> Tasks { get; set; } = new List<Tasks>();
}