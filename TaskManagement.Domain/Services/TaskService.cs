using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Domain.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICommentRepository _commentRepository;

    public TaskService(ITaskRepository taskRepository, ICommentRepository commentRepository)
    {
        _taskRepository = taskRepository;
        _commentRepository = commentRepository;
    }

    public async Task AddCommentToTaskAsync(Guid taskId, string commentText)
    {
        // Get the task
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        // Create the comment
        var comment = new Comments
        {
            TTaskId = taskId,
            Comment = commentText,
            DueDate = DateTime.UtcNow
        };

        // Add the comment to the task
        await _commentRepository.AddCommentAsync(comment);

        // Create a task history entry
        var taskHistory = new TaskHistory
        {
            TaskId = taskId,
            ChangeDetails = commentText,
            ChangeDate = DateTime.UtcNow
        };

        // Add the history to the task
        await _taskRepository.AddTaskHistoryAsync(taskHistory);

        // Save changes to the database
        await _commentRepository.SaveAsync();
    }   
}