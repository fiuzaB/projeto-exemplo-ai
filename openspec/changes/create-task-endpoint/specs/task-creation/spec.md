## ADDED Requirements

### Requirement: Criar tarefa via POST
O sistema SHALL aceitar `POST /api/tasks` com corpo JSON contendo `title`, `description` e `dueDate`, criar a tarefa com `status` fixo em `"Pendente"` e retornar `201 Created` com o objeto completo incluindo `id` (Guid), `createdAt` (UTC) e `status`.

#### Scenario: Criação com todos os campos válidos
- **WHEN** o cliente envia `POST /api/tasks` com `title` de 3–100 chars, `description` de até 500 chars e `dueDate` futura
- **THEN** o sistema retorna `201 Created` com `id` (Guid), `title`, `description`, `dueDate`, `createdAt` (UTC) e `status: "Pendente"`

#### Scenario: Criação apenas com o campo obrigatório
- **WHEN** o cliente envia `POST /api/tasks` com apenas `title` válido, sem `description` nem `dueDate`
- **THEN** o sistema retorna `201 Created` com `description: null`, `dueDate: null` e `status: "Pendente"`

### Requirement: Validar title obrigatório e tamanho
O sistema SHALL rejeitar requisições onde `title` esteja ausente, vazio, com menos de 3 caracteres ou mais de 100 caracteres.

#### Scenario: title ausente ou vazio
- **WHEN** o cliente envia `POST /api/tasks` sem `title` ou com `title: ""`
- **THEN** o sistema retorna `400 Bad Request` com mensagem indicando que `title` é obrigatório

#### Scenario: title abaixo do mínimo
- **WHEN** o cliente envia `title` com menos de 3 caracteres
- **THEN** o sistema retorna `400 Bad Request` com mensagem indicando o mínimo de 3 caracteres

#### Scenario: title acima do máximo
- **WHEN** o cliente envia `title` com mais de 100 caracteres
- **THEN** o sistema retorna `400 Bad Request` com mensagem indicando o máximo de 100 caracteres

### Requirement: Validar description com tamanho máximo
O sistema SHALL rejeitar requisições onde `description` seja fornecida com mais de 500 caracteres.

#### Scenario: description acima do máximo
- **WHEN** o cliente envia `description` com mais de 500 caracteres
- **THEN** o sistema retorna `400 Bad Request` com mensagem indicando o máximo de 500 caracteres

### Requirement: Validar dueDate como data futura
O sistema SHALL rejeitar requisições onde `dueDate` seja fornecida e não seja estritamente futura em relação ao momento da requisição (UTC).

#### Scenario: dueDate no passado
- **WHEN** o cliente envia `dueDate` anterior à data/hora atual UTC
- **THEN** o sistema retorna `400 Bad Request` com mensagem indicando que `dueDate` deve ser futura

#### Scenario: dueDate igual ao momento atual
- **WHEN** o cliente envia `dueDate` igual à data/hora atual UTC
- **THEN** o sistema retorna `400 Bad Request` com mensagem indicando que `dueDate` deve ser futura
