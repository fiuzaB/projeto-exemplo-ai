## Why

A API expõe apenas `GET /api/tasks`, sem permitir que clientes criem novas tarefas. O endpoint `POST /api/tasks` é o próximo passo natural para tornar a API utilizável.

## What Changes

- Novo endpoint `POST /api/tasks` que recebe `title`, `description` e `dueDate`
- Validação de entrada: `title` obrigatório (3–100 chars), `description` opcional (máx 500 chars), `dueDate` opcional mas deve ser data futura
- Status inicial sempre fixo em `"Pendente"`
- Resposta `201 Created` com o objeto completo: `id` (Guid), `title`, `description`, `dueDate`, `createdAt` (UTC) e `status`
- Resposta `400 Bad Request` com mensagens de erro para violações de validação

## Capabilities

### New Capabilities

- `task-creation`: Criação de tarefas via `POST /api/tasks` com validação dos campos e persistência no arquivo JSON

### Modified Capabilities

- `task-storage`: A persistência existente precisa suportar escrita de novas tarefas; o modelo passa a usar `id` como Guid e adiciona o campo `createdAt`

## Impact

- `TodoApi/Models/TodoTask.cs`: alterar `Id` de `int` para `Guid`, adicionar `CreatedAt`
- `TodoApi/Models/CreateTaskRequest.cs`: novo DTO de entrada
- `TodoApi/Repositories/ITaskRepository.cs` e `TaskRepository.cs`: adicionar método `Create`
- `TodoApi/Services/ITaskService.cs` e `TaskService.cs`: adicionar método `Create` com validação
- `TodoApi/Endpoints/TaskEndpoints.cs`: registrar o endpoint `POST /api/tasks`
- `TodoApi/todolist.json`: atualizar dados de exemplo para o novo formato (Guid, createdAt)
