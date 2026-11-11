using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyHotels.Gestao.Entities;

namespace MyHotels.Gestao.Data;

public static class Inicializacao
{
    public static async Task CriaDadosIniciais(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Administrador", "Funcionario", "Cliente", "Fornecedor" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "admin@localhost.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                Nome = "Administrador Principal",
                Estado = "Activo" // Alterado para Activo conforme a Ficha 5
            };
            var result = await userManager.CreateAsync(adminUser, "Is3C..00");
            if (result.Succeeded) await userManager.AddToRoleAsync(adminUser, "Administrador");
        }
    }

    // Novo método para criar dados de exemplo para os Hotéis
    public static async Task CriaDadosExemplo(ApplicationDbContext context)
    {
        if (await context.Categorias.AnyAsync()) return; // Se já há dados, não duplica

        var catHoteis = new Categoria { Nome = "Hotéis", Ordem = 1 };
        var catApart = new Categoria { Nome = "Apartamentos", Ordem = 2 };

        var modoTI = new ModoDisponibilizacao { Nome = "Tudo Incluído" };
        var modoAPA = new ModoDisponibilizacao { Nome = "Alojamento e Pequeno-Almoço" };

        context.Categorias.AddRange(catHoteis, catApart);
        context.ModosDisponibilizacao.AddRange(modoTI, modoAPA);

        context.Produtos.AddRange(
            new Produto { Nome = "Quarto Duplo Vista Mar", Detalhe = "Quarto espaçoso com vista mar", PrecoBase = 120.50m, Disponibilidade = 10, Visivel = true, Categoria = catHoteis, ModoDisponibilizacao = modoAPA },
            new Produto { Nome = "T0 Centro da Cidade", Detalhe = "Estúdio moderno no centro", PrecoBase = 75.00m, Disponibilidade = 5, Visivel = true, Categoria = catApart, ModoDisponibilizacao = null }
        );

        await context.SaveChangesAsync();
    }
}