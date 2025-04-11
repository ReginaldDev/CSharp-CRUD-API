Markdown

# 📦 Projeto CRUD com API .NET e SQL Server + Cliente Terminal C#

Este projeto é um CRUD simples utilizando **ASP.NET Core Web API** no backend e um cliente de terminal (console) em C# para consumo da API. O banco de dados utilizado é o **SQL Server**, e a comunicação é feita com o **Entity Framework Core (EF Core)**.

---

## 🗂 Estrutura do Projeto

```text
ApiCadastroProdutos/
├── Controllers/
│   └── ProdutoController.cs      # Controller da API para gerenciar produtos
├── Data/
│   └── ProdutoDbContext.cs       # Contexto do Entity Framework Core para interação com o banco
├── Models/
│   └── Produto.cs                # Modelo de dados da tabela Produtos
├── Migrations/                   # Arquivos de migration do EF Core para o banco de dados
│   ├── [timestamp]_InitialCreate.cs
│   └── ProdutoDbContextModelSnapshot.cs
├── Properties/
│   └── launchSettings.json       # Configurações de execução da API
├── appsettings.Development.json  # Configurações da aplicação para desenvolvimento
├── appsettings.json              # Configurações da aplicação
├── ApiCadastroProdutos.csproj    # Arquivo de projeto da API
├── Program.cs                    # Ponto de entrada principal da API
└── ...

TerminalCadastroProdutos/
├── Program.cs                    # Script cliente que consome a API via terminal
├── TerminalCadastroProdutos.csproj # Arquivo de projeto do cliente terminal
├── Produto.cs                    # Modelo de dados local para comunicação com a API
└── ...

README.md                         # Este arquivo
🧱 Criação da Tabela SQL
O Entity Framework Core irá criar a tabela Produtos automaticamente com base no modelo Models/Produto.cs através das Migrations. No entanto, certifique-se de ter um banco de dados chamado CadastroDeProdutos configurado no seu SQL Server. A estrutura da tabela Produtos será semelhante a:

SQL

CREATE TABLE Produtos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(MAX) NOT NULL,
    Descricao NVARCHAR(MAX) NOT NULL,
    Preco DECIMAL(18, 2) NOT NULL
);
⚙️ Configurando a Conexão com o Banco de Dados
Edite o arquivo appsettings.json (ou appsettings.Development.json para ambiente de desenvolvimento) dentro da pasta ApiCadastroProdutos e altere a seção ConnectionStrings com as informações do seu ambiente SQL Server:

JSON

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
💡 Dica: Substitua SEU_SERVIDOR, CadastroDeProdutos, SEU_USUARIO e SUA_SENHA pelas suas credenciais e nome do servidor. Para autenticação do Windows, use Integrated Security=True;.

▶️ Passos para Rodar o Projeto
1. Navegue até a pasta da API
Bash

cd ApiCadastroProdutos
2. Execute as Migrations (para criar o banco de dados e a tabela)
Bash

dotnet ef database update
⚠️ Certifique-se de ter as ferramentas do Entity Framework Core instaladas (dotnet tool install --global dotnet-ef).

3. Inicie a API
Rode o seguinte comando na pasta ApiCadastroProdutos:

Bash

dotnet run
A API estará disponível em:
👉 http://localhost:[porta]/api/produto (a porta pode variar, verifique a saída do comando dotnet run).

4. Navegue até a pasta do Cliente Terminal
Abra um novo terminal e navegue até a pasta TerminalCadastroProdutos:

Bash

cd TerminalCadastroProdutos
5. Execute o cliente Python no terminal
Rode o seguinte comando:

Bash

dotnet run
Esse script permite interagir com a API diretamente pelo terminal, com opções para listar, buscar, criar, atualizar e remover produtos.

✅ Funcionalidades da API
GET /api/produto – Listar todos os produtos
GET /api/produto/{id} – Buscar produto por ID
POST /api/produto – Criar novo produto (envie um objeto JSON no corpo da requisição)
PUT /api/produto/{id} – Atualizar dados de um produto (envie um objeto JSON no corpo da requisição)
DELETE /api/produto/{id} – Remover produto por ID
💬 Suporte
Se tiver dúvidas ou quiser expandir este projeto, sinta-se à vontade para perguntar!

📬 Contato
[Seu Nome/Contato]
[Seu Email/Outro Meio de Contato]