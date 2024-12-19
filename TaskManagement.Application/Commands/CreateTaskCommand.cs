using FluentValidation;
using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enum;

namespace TaskManagementAPI.Application.Commands;

public class CreateTaskCommand : IRequest<Tasks>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public StatusTask Status { get; set; }
}

public class CreateTarefaCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTarefaCommandValidator()
    {
        // Validação para o título da tarefa
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("O título da tarefa é obrigatório.")
            .MaximumLength(150).WithMessage("O título da tarefa deve ter no máximo 150 caracteres.");

        // Validação para a descrição da tarefa
        RuleFor(t => t.Description)
            .MaximumLength(500).WithMessage("A descrição da tarefa deve ter no máximo 500 caracteres.");

        // Validação para a data de vencimento (deve ser uma data futura)
        RuleFor(t => t.DueDate)
            .GreaterThan(DateTime.Now).WithMessage("A data de vencimento deve ser no futuro.");

        // Validação para a prioridade da tarefa
        RuleFor(t => t.Priority)
            .NotEmpty().WithMessage("A prioridade da tarefa é obrigatória.")
            .IsInEnum().WithMessage("A prioridade deve ser um valor válido: BAIXA = 1, MEDIA = 2 ou ALTA = 3.");

        // Validação para o status da tarefa
        RuleFor(t => t.Status)
            .NotEmpty().WithMessage("O status da tarefa é obrigatório.")
            .Must(status => status == StatusTask.PENDENTE || status == StatusTask.EM_ANDAMENTO || status == StatusTask.CONCLUIDA)
            .WithMessage("O status deve ser 'PENDENTE = 1', 'EM_ANDAMENTO = 2' ou 'CONCLUIDA = 1'.");
    }
}