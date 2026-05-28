using System.Text.Json;
using TodoApi.Models;

namespace TodoApi.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public TaskRepository(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, "todolist.json");
    }

    public IEnumerable<TodoTask> GetAll()
    {
        if (!File.Exists(_filePath))
            return [];

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TodoTask>>(json, _jsonOptions) ?? [];
    }

    public TodoTask Create(TodoTask task)
    {
        var tasks = GetAll().ToList();
        tasks.Add(task);
        File.WriteAllText(_filePath, JsonSerializer.Serialize(tasks, _jsonOptions));
        return task;
    }
}
