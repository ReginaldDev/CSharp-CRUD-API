# 📦 Projeto CRUD com API .NET e SQL Server + Cliente Terminal C#

Este projeto demonstra a implementação de um sistema **CRUD (Create, Read, Update, Delete)** utilizando **ASP.NET Core Web API** no backend, com um cliente de terminal (console) em **C#** para consumo da API. O banco de dados utilizado é o **SQL Server**, com acesso via **Entity Framework Core (EF Core)**.

---

## 🗂 Estrutura do Projeto

```
ApiCadastroProdutos/
├── Controllers/
│   └── ProdutoController.cs            # Controller da API para gerenciar produtos
├── Data/
│   └── ProdutoDbContext.cs             # Contexto do EF Core para interação com o banco
├── Models/
│   └── Produto.cs                      # Modelo de dados da tabela Produtos
├── Migrations/                         # Arquivos de migration do EF Core
│   ├── [timestamp]_InitialCreate.cs
│   └── ProdutoDbContextModelSnapshot.cs
├── Properties/
│   └── launchSettings.json             # Configurações de execução da API
├── appsettings.Development.json        # Configurações de desenvolvimento
├── appsettings.json                    # Configurações gerais da aplicação
├── ApiCadastroProdutos.csproj          # Arquivo de projeto da API
├── Program.cs                          # Ponto de entrada da aplicação
└── ...

TerminalCadastroProdutos/
├── Program.cs                          # Cliente que consome a API via terminal
├── TerminalCadastroProdutos.csproj     # Arquivo de projeto do cliente terminal
├── Produto.cs                          # Modelo local para comunicação com a API
└── ...

README.md                               # Este arquivo
```

---

## 🧱 Criação da Tabela no SQL Server

O **EF Core** criará a tabela `Produtos` automaticamente com base no modelo `Produto.cs`. Certifique-se de ter um banco chamado `CadastroDeProdutos` criado no SQL Server.

### Estrutura da Tabela `Produtos`:

```sql
CREATE TABLE Produtos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(MAX) NOT NULL,
    Descricao NVARCHAR(MAX) NOT NULL,
    Preco DECIMAL(18, 2) NOT NULL
);
```

---

## ⚙️ Configuração da Conexão com o Banco de Dados

Altere o arquivo `appsettings.json` ou `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=CadastroDeProdutos;User ID=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True"
  }
}
```

> 💡 Dica: Para autenticação do Windows, utilize:  
> `"DefaultConnection": "Server=SEU_SERVIDOR;Database=CadastroDeProdutos;Integrated Security=True;"`

---

## ▶️ Como Rodar o Projeto

### 1. Navegue até a pasta da API

```bash
cd ApiCadastroProdutos
```

### 2. Execute as Migrations (criação do banco e da tabela)

```bash
dotnet ef database update
```

> ⚠️ Certifique-se de ter o EF Core instalado:  
> `dotnet tool install --global dotnet-ef`

### 3. Inicie a API

```bash
dotnet run
```

A API estará disponível em:
```
http://localhost:[porta]/api/produto
```
> (A porta será exibida ao rodar o comando.)

### 4. Navegue até a pasta do Cliente Terminal

```bash
cd TerminalCadastroProdutos
```

### 5. Execute o Cliente Terminal

```bash
dotnet run
```

O cliente permite interagir com a API via terminal: listar, buscar, criar, atualizar e excluir produtos.

---

## ✅ Funcionalidades da API

| Método | Rota                        | Descrição                          |
|--------|-----------------------------|------------------------------------|
| GET    | `/api/produto`              | Listar todos os produtos           |
| GET    | `/api/produto/{id}`         | Buscar produto por ID              |
| POST   | `/api/produto`              | Criar novo produto                 |
| PUT    | `/api/produto/{id}`         | Atualizar produto existente        |
| DELETE | `/api/produto/{id}`         | Remover produto por ID             |

> Todos os métodos que enviam dados (POST/PUT) devem receber um objeto JSON com os campos: `Nome`, `Descricao`, `Preco`.

---

## 💬 Suporte

Se tiver dúvidas, sugestões ou quiser expandir este projeto, fique à vontade para entrar em contato!

---

## 📬 Contato

**Nome:** [Reginaldo Junior]  
**Email:** [reginaldo.devtech@gmail.com]  
**GitHub:** [github.com/ReginaldDev](https://github.com/ReginaldDev)

---
