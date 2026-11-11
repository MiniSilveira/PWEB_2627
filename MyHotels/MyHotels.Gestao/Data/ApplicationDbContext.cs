using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHotels.Gestao.Entities;

namespace MyHotels.Gestao.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<ModoDisponibilizacao> ModosDisponibilizacao { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Morada> Moradas { get; set; } // Tabela nova adicionada

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // MUITO IMPORTANTE: Nunca apagar esta linha, senão o Identity dá erro ("IdentityUserLogin requires a primary key")
        base.OnModelCreating(builder);

        // Regra 1: Não podem existir duas categorias com o mesmo nome exato
        builder.Entity<Categoria>()
            .HasIndex(c => c.Nome)
            .IsUnique();

        // Regra 2: Garantia na Base de Dados de que o preço é sempre maior que zero
        builder.Entity<Produto>().ToTable(t => t.HasCheckConstraint(
            "CK_Produtos_PrecoBase", "[PrecoBase] > 0"));
    }
}