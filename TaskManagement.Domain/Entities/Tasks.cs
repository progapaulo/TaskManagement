using System.Text.Json.Serialization;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Converters;
using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.Entities;

public class Tasks : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    [JsonConverter(typeof(EnumDescriptionConverter))]
    public StatusTask Status { get; set; }
    [JsonConverter(typeof(EnumDescriptionConverter))]
    public Priority Priority { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public List<TaskHistory> History { get; set; } = new List<TaskHistory>();
    public List<Comments> Comentarios { get; set; }
    
    public Tasks()
    {
        Comentarios = new List<Comments>();
        History = new List<TaskHistory>();
    }
}