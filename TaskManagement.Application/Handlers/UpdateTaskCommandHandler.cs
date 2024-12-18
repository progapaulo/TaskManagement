using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagementAPI.Application.Commands;

namespace TaskManagementAPI.Application.Handlers;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Tasks>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Tasks> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)
        {
            throw new ArgumentException("Tarefa não encontrada.");
        }

        task.Title = request.Title;
        task.Description = request.Description;
        task.DueDate = request.DueDate;
        task.Status = request.Status;

        return await _taskRepository.UpdateAsync(task);
    }
}