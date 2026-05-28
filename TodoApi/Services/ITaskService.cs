using TodoApi.Models;

namespace TodoApi.Services;

public interface ITaskService
{
    IEnumerable<TodoTask> GetAll();
}
