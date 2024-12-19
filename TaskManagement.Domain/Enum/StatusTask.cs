using System.ComponentModel;
using Newtonsoft.Json;

namespace TaskManagement.Domain.Enum;

public enum StatusTask
{
    [Description("Pendente")]
    PENDENTE  = 0, 
    [Description("Em Andamento")]
    EM_ANDAMENTO,
    [Description("Concluido")]
    CONCLUIDA
}