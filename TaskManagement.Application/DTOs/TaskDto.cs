namespace TaskManagementAPI.Application.DTOs;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public Guid ProjectId { get; set; }
}