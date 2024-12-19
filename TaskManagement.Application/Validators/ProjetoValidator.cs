using FluentValidation;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Validators;

public class ProjetoValidator : AbstractValidator<Project>
{
    public ProjetoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome do projeto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do projeto deve ter no máximo 100 caracteres.");

        RuleFor(p => p.UsuarioId)
            .GreaterThan(new Guid()).WithMessage("O projeto deve estar associado a um usuário.");

        RuleFor(p => p.Tasks)
            .Must(tarefas => tarefas.Count <= 20)
            .WithMessage("O projeto não pode ter mais de 20 tarefas.");
    }
}