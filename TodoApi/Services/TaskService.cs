using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TaskService(ITaskRepository repository) : ITaskService
{
    public IEnumerable<TodoTask> GetAll() => repository.GetAll();

    public (TodoTask? task, List<string> errors) Create(CreateTaskRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.Title))
            errors.Add("O campo 'title' é obrigatório.");
        else if (request.Title.Length < 3)
            errors.Add("O campo 'title' deve ter no mínimo 3 caracteres.");
        else if (request.Title.Length > 100)
            errors.Add("O campo 'title' deve ter no máximo 100 caracteres.");

        if (request.Description is not null && request.Description.Length > 500)
            errors.Add("O campo 'description' deve ter no máximo 500 caracteres.");

        if (request.DueDate is not null && request.DueDate <= DateTime.UtcNow)
            errors.Add("O campo 'dueDate' deve ser uma data futura.");

        if (errors.Count > 0)
            return (null, errors);

        var task = new TodoTask
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow,
            Status = "Pendente"
        };

        repository.Create(task);
        return (task, errors);
    }
}
