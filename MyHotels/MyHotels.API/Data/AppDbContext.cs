using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHotels.API.Entities;

namespace MyHotels.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<ModoDisponibilizacao> ModosDisponibilizacao { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Morada> Moradas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Copiamos as mesmas regras da Gestão de Loja para garantir consistência
        builder.Entity<Categoria>()
            .HasIndex(c => c.Nome)
            .IsUnique();

        builder.Entity<Produto>().ToTable(t => t.HasCheckConstraint(
            "CK_Produtos_PrecoBase", "[PrecoBase] > 0"));
    }
}