using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Entities;

public class Comments : BaseEntity
{
    public string Comment { get; set; }
    public DateTime DueDate { get; set; }
    public Guid TTaskId { get; set; }
    public Tasks Tarefa { get; set; }
}