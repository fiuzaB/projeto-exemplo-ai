## MODIFIED Requirements

### Requirement: Persistir e ler tarefas no arquivo JSON
O sistema SHALL ler e escrever tarefas no arquivo `todolist.json` usando o formato atualizado: `id` como Guid, `title`, `description` (nullable), `dueDate` (nullable), `createdAt` (UTC) e `status`. O repositório SHALL expor o método `Create(TodoTask)` que lê o arquivo, acrescenta a nova tarefa e reescreve o conteúdo completo.

#### Scenario: Leitura de tarefas existentes
- **WHEN** `GET /api/tasks` é chamado
- **THEN** o repositório lê `todolist.json` e desserializa a lista completa no novo formato com todos os campos

#### Scenario: Criação persiste no arquivo
- **WHEN** `TaskRepository.Create(task)` é invocado com uma tarefa válida
- **THEN** o repositório lê a lista atual, acrescenta a nova tarefa e reescreve `todolist.json` com a lista atualizada

#### Scenario: Arquivo inexistente ao criar
- **WHEN** `TaskRepository.Create(task)` é invocado e `todolist.json` não existe
- **THEN** o repositório cria o arquivo com uma lista contendo apenas a nova tarefa
