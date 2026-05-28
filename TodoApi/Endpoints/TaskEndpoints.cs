using TodoApi.Services;

namespace TodoApi.Endpoints;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this WebApplication app)
    {
        app.MapGet("api/tasks", (ITaskService service) =>
        {
            var tasks = service.GetAll();
            return Results.Ok(tasks);
        });
    }
}
