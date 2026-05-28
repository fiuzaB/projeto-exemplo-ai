using TodoApi.Models;
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

        app.MapPost("api/tasks", (CreateTaskRequest request, ITaskService service) =>
        {
            var (task, errors) = service.Create(request);

            if (errors.Count > 0)
                return Results.BadRequest(new { errors });

            return Results.Created($"/api/tasks/{task!.Id}", task);
        });
    }
}
