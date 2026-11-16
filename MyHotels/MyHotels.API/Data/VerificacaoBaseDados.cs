using Microsoft.EntityFrameworkCore;

namespace MyHotels.API.Data;

public static class VerificacaoBaseDados
{
    public static async Task ExecutarAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        try
        {
            if (!await context.Database.CanConnectAsync())
                throw new Exception("Não foi possível ligar à base de dados.");

            var pendentes = await context.Database.GetPendingMigrationsAsync();
            if (pendentes.Any())
                throw new Exception("Faltam migrations! Corra Update-Database no projeto da Gestão de Loja antes de arrancar a API.");
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogCritical(ex, "Erro fatal: a API não consegue usar a base de dados.");
            throw; // Trava o arranque da API se houver problemas
        }
    }
}