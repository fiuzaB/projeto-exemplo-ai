## Context

A API já possui um repositório baseado em arquivo JSON (`todolist.json`) com leitura via `TaskRepository`. O modelo `TodoTask` tem os campos `description`, `dueDate` e `status`. A stack é .NET 8 Minimal API sem dependências externas de validação instaladas.

## Goals / Non-Goals

**Goals:**
- Implementar `POST /api/tasks` com validação de entrada na camada de serviço
- Gerar `id` como `Guid` e `createdAt` como `DateTime` UTC no momento da criação
- Persistir a nova tarefa no `todolist.json`
- Retornar `201 Created` com o objeto completo ou `400 Bad Request` com mensagens de erro

**Non-Goals:**
- Autenticação ou autorização
- Outros verbos CRUD (PUT, DELETE, GET por id)
- Banco de dados relacional ou controle de concorrência no arquivo

## Decisions

**Guid para Id em vez de int auto-incrementado**
Gerar o próximo `int` exigiria ler toda a lista e calcular o máximo — frágil com arquivo JSON. `Guid.NewGuid()` é stateless e não precisa de coordenação. Impacto: `TodoTask.Id` muda de `int` para `Guid`; o `todolist.json` e o GET precisam refletir isso.

**Validação manual no serviço, sem biblioteca externa**
O projeto não tem FluentValidation nem Data Annotations configurados para Minimal APIs. Validação explícita em `TaskService` mantém zero novas dependências e é suficiente para as três regras definidas. Retorna uma lista de erros para que o endpoint possa responder `400` com todas as violações de uma vez.

**DTO de entrada separado do modelo de domínio**
`CreateTaskRequest` expõe apenas os campos que o cliente pode enviar (`title`, `description`, `dueDate`). Campos gerados pelo servidor (`id`, `createdAt`, `status`) não são aceitos como entrada, evitando mass-assignment.

## Risks / Trade-offs

[Concorrência no arquivo JSON] → O repositório lê e reescreve o arquivo sem lock. Aceitável para o escopo didático do projeto; em produção exigiria `SemaphoreSlim` ou banco de dados.

[Dados existentes no JSON com `id` int] → A migração do `todolist.json` para Guids deve acontecer junto com a alteração do modelo, ou o `JsonSerializer` falhará na desserialização do GET.
