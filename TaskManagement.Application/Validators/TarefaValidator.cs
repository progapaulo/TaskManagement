using FluentValidation;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enum;

namespace TaskManagementAPI.Application.Validators;

public class TarefaValidator : AbstractValidator<Tasks>
{
    public TarefaValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("O título da tarefa é obrigatório.")
            .MaximumLength(150).WithMessage("O título da tarefa deve ter no máximo 150 caracteres.");

        RuleFor(t => t.Description)
            .MaximumLength(500).WithMessage("A descrição da tarefa deve ter no máximo 500 caracteres.");

        RuleFor(t => t.DueDate)
            .NotEmpty().WithMessage("A data de vencimento da tarefa é obrigatória.")
            .GreaterThan(DateTime.Now).WithMessage("A data de vencimento deve ser futura.");

        RuleFor(t => t.Priority)
            .IsInEnum().WithMessage("A prioridade deve ser válida.");

        RuleFor(t => t.Status)
            .NotEmpty().WithMessage("O status da tarefa é obrigatório.")
            .Must(status => status == StatusTask.PENDENTE || status == StatusTask.EM_ANDAMENTO || status == StatusTask.CONCLUIDA)
            .WithMessage("O status deve ser 'PENDENTE', 'EM_ANDAMENTO' ou 'CONCLUIDA'.");
    }
}