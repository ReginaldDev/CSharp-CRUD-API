using Microsoft.EntityFrameworkCore;
using ApiCadastroProdutos.Models;

namespace ApiCadastroProdutos.Data
{
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options)
        {

        }
        public DbSet<Produto> Produtos { get; set; }
    }        
}
#pragma warning restore format