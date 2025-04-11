using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace TerminalCadastroProdutos
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5123/api/produto"; // Ajuste a URL da sua API

        static async Task Main(string[] args)
        {
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                Console.WriteLine("\nOpções:");
                Console.WriteLine("1. Listar Produtos");
                Console.WriteLine("2. Obter Produto por ID");
                Console.WriteLine("3. Criar Produto");
                Console.WriteLine("4. Atualizar Produto");
                Console.WriteLine("5. Excluir Produto");
                Console.WriteLine("6. Sair");
                Console.Write("Escolha uma opção: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListarProdutos();
                        break;
                    case "2":
                        await ObterProdutoPorId();
                        break;
                    case "3":
                        await CriarProduto();
                        break;
                    case "4":
                        await AtualizarProduto();
                        break;
                    case "5":
                        await ExcluirProduto();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        static async Task ListarProdutos()
        {
            try
            {
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var produtos = await response.Content.ReadFromJsonAsync<List<Produto>>();

                if (produtos != null && produtos.Any())
                {
                    Console.WriteLine("\nLista de Produtos:");
                    foreach (var produto in produtos)
                    {
                        Console.WriteLine($"ID: {produto.Id}, Nome: {produto.Nome}, Descrição: {produto.Descricao}, Preço: {produto.Preco:C}");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum produto encontrado.");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro ao listar produtos: {e.Message}");
            }
        }

        static async Task ObterProdutoPorId()
        {
            Console.Write("Digite o ID do produto: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var response = await client.GetAsync($"{apiUrl}/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var produto = await response.Content.ReadFromJsonAsync<Produto>();
                        if (produto != null)
                        {
                            Console.WriteLine($"\nProduto:");
                            Console.WriteLine($"ID: {produto.Id}, Nome: {produto.Nome}, Descrição: {produto.Descricao}, Preço: {produto.Preco:C}");
                        }
                        else
                        {
                            Console.WriteLine($"Produto com ID {id} não encontrado.");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Produto com ID {id} não encontrado.");
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao obter produto: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro ao obter produto: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        static async Task CriarProduto()
        {
            Console.Write("Digite o nome do produto: ");
            string? nome = Console.ReadLine();
            Console.Write("Digite a descrição do produto: ");
            string? descricao = Console.ReadLine();
            Console.Write("Digite o preço do produto: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal preco))
            {
                var novoProduto = new Produto { Nome = nome, Descricao = descricao, Preco = preco };
                try
                {
                    var response = await client.PostAsJsonAsync(apiUrl, novoProduto);
                    response.EnsureSuccessStatusCode();
                    var produtoCriado = await response.Content.ReadFromJsonAsync<Produto>();
                    Console.WriteLine($"\nProduto criado com ID: {produtoCriado?.Id}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro ao criar produto: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Preço inválido.");
            }
        }

        static async Task AtualizarProduto()
        {
            Console.Write("Digite o ID do produto a ser atualizado: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Digite o novo nome do produto: ");
                string? nome = Console.ReadLine();
                Console.Write("Digite a nova descrição do produto: ");
                string? descricao = Console.ReadLine();
                Console.Write("Digite o novo preço do produto: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal preco))
                {
                    var produtoAtualizado = new Produto { Id = id, Nome = nome, Descricao = descricao, Preco = preco };
                    try
                    {
                        var response = await client.PutAsJsonAsync($"{apiUrl}/{id}", produtoAtualizado);
                        response.EnsureSuccessStatusCode();
                        Console.WriteLine($"\nProduto com ID {id} atualizado com sucesso.");
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Erro ao atualizar produto: {e.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Preço inválido.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        static async Task ExcluirProduto()
        {
            Console.Write("Digite o ID do produto a ser excluído: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var response = await client.DeleteAsync($"{apiUrl}/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"\nProduto com ID {id} excluído com sucesso.");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Produto com ID {id} não encontrado.");
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao excluir produto: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro ao excluir produto: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
    }

    public class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
