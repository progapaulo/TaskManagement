using FluentValidation;
using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagementAPI.Application.Commands;

public class CreateProjectCommand : IRequest<Project>
{
    public string Name { get; set; }
    public Guid UserID { get; set; }
}
public class CreateProjetoCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjetoCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome do projeto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do projeto deve ter no máximo 100 caracteres.");

        RuleFor(p => p.UserID)
            .GreaterThan(new Guid()).WithMessage("O projeto deve estar associado a um usuário.");
    }
}
