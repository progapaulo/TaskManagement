using System.ComponentModel;
using Newtonsoft.Json;

namespace TaskManagement.Domain.Enum;

public enum Priority
{
    [Description("Baixa")]
    BAIXA = 0, 
    [Description("Média")]
    MEDIA, 
    [Description("Alta")]
    ALTA
}