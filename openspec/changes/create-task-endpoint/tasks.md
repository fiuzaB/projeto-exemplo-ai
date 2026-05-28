## 1. Atualizar o Modelo

- [x] 1.1 Alterar `TodoTask.Id` de `int` para `Guid`
- [x] 1.2 Adicionar propriedade `CreatedAt` (DateTime) ao modelo `TodoTask`
- [x] 1.3 Atualizar `todolist.json` com dados de exemplo no novo formato (Guid, createdAt)

## 2. Criar o DTO de Entrada

- [x] 2.1 Criar `Models/CreateTaskRequest.cs` com os campos `Title` (string), `Description` (string?) e `DueDate` (DateTime?)

## 3. Atualizar o Repositório

- [x] 3.1 Adicionar método `Create(TodoTask task)` à interface `ITaskRepository`
- [x] 3.2 Implementar `Create` em `TaskRepository`: ler lista atual, acrescentar a tarefa e reescrever o arquivo; criar o arquivo se não existir

## 4. Atualizar o Serviço

- [x] 4.1 Adicionar método `Create(CreateTaskRequest request)` à interface `ITaskService`
- [x] 4.2 Implementar validações em `TaskService`: `title` obrigatório (3–100 chars), `description` máx 500 chars, `dueDate` deve ser futura (UTC)
- [x] 4.3 Em caso de sucesso, montar `TodoTask` com `Guid.NewGuid()`, `DateTime.UtcNow` e `Status: "Pendente"` e chamar `repository.Create`

## 5. Registrar o Endpoint POST

- [x] 5.1 Adicionar `app.MapPost("api/tasks", ...)` em `TaskEndpoints.cs` que chama `service.Create(request)` e retorna `201 Created` com o objeto criado ou `400 Bad Request` com as mensagens de validação
