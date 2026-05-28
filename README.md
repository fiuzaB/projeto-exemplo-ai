# projeto-exemplo-ai

API REST de lista de tarefas construída com .NET 8 Minimal API, usada como projeto de exemplo para demonstrar o fluxo de desenvolvimento assistido por IA com **OpenSpec**.

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org) (para o OpenSpec CLI)
- [OpenSpec CLI](https://openspec.dev) — instale globalmente:

```bash
npm install -g openspec
```

---

## Instalação e execução

```bash
# 1. Clone o repositório
git clone <url-do-repositorio>
cd projeto-exemplo-ai

# 2. Entre na pasta da API
cd TodoApi

# 3. Restaure as dependências e execute
dotnet run
```

A API estará disponível em `http://localhost:5285`.

---

## Endpoints disponíveis

| Método | Rota          | Descrição              |
|--------|---------------|------------------------|
| GET    | /api/tasks    | Lista todas as tarefas |

---

## Estrutura do projeto

```
projeto-exemplo-ai/
├── TodoApi/
│   ├── Endpoints/         # Mapeamento de rotas (Minimal API)
│   ├── Models/            # Modelos de domínio
│   ├── Repositories/      # Acesso a dados (arquivo JSON)
│   ├── Services/          # Regras de negócio
│   ├── todolist.json      # Banco de dados em arquivo
│   └── Program.cs         # Ponto de entrada e configuração de DI
└── openspec/              # Artefatos de mudanças gerenciadas pelo OpenSpec
    └── changes/           # Histórico de propostas e tarefas de implementação
```

---

## Formato da apresentação

> Demo ao vivo — duração aproximada: **5 minutos**  
> Público: alunos

### Roteiro

**1. Estrutura do projeto (1 min)**
Apresentar as camadas da aplicação: `Models`, `Repositories`, `Services` e `Endpoints`. Mostrar como o `Program.cs` conecta tudo via injeção de dependência e como os dados são persistidos no `todolist.json`.

**2. GET funcionando (1 min)**
Subir a aplicação com `dotnet run` e chamar `GET /api/tasks` ao vivo, mostrando a resposta com os campos `title`, `description`, `dueDate` e `status`.

**3. Introdução ao OpenSpec + implementação do POST (3 min)**
Explicar o problema: precisamos criar um endpoint `POST /api/tasks` com validação de campos. Mostrar como o OpenSpec estrutura esse trabalho em etapas antes de escrever uma linha de código:

```bash
# Propor a mudança — gera proposal.md, design.md, specs/ e tasks.md
/opsx:propose

# Implementar tarefa a tarefa seguindo o tasks.md gerado
/opsx:apply
```

Destacar que o OpenSpec força clareza antes do código: **o que** (specs), **por quê** (proposal) e **como** (design) ficam documentados antes da implementação começar.

#### Proposta usada na demo

Ao invocar `/opsx:propose`, utilize exatamente o seguinte texto como argumento:

```
POST /api/tasks · Cria uma tarefa com os campos title (obrigatório, 3–100 chars),
description (opcional, máx 500 chars) e dueDate (opcional, deve ser futura).
O status inicial é sempre "Pendente". Retorne 201 com o objeto criado incluindo
id (Guid), createdAt (UTC) e status. Retorne 400 para violações de validação.
```

Esse texto é suficiente para o OpenSpec gerar automaticamente o `proposal.md`, `design.md`, `specs/` e `tasks.md` antes de qualquer linha de código ser escrita.
