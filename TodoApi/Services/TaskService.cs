using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TaskService(ITaskRepository repository) : ITaskService
{
    public IEnumerable<TodoTask> GetAll() => repository.GetAll();
}
