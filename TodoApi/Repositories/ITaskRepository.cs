using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITaskRepository
{
    IEnumerable<TodoTask> GetAll();
}
