using Microsoft.EntityFrameworkCore;
using MyHotels.API.Data;
using MyHotels.API.Entities;

namespace MyHotels.API.Repositories;

public class ModoDisponibilizacaoRepository(AppDbContext context) : IModoDisponibilizacaoRepository
{
    public Task<List<ModoDisponibilizacao>> GetAllAsync() =>
        context.ModosDisponibilizacao.OrderBy(m => m.Nome).AsNoTracking().ToListAsync();

    public Task<ModoDisponibilizacao?> GetByIdAsync(int id) =>
        context.ModosDisponibilizacao.FirstOrDefaultAsync(m => m.Id == id);

    public Task<bool> NomeExisteAsync(string nome, int? excetoId = null) =>
        context.ModosDisponibilizacao.AnyAsync(m => m.Nome == nome && m.Id != excetoId);

    public Task<bool> ExisteAsync(int id) =>
        context.ModosDisponibilizacao.AnyAsync(m => m.Id == id);

    public Task<bool> TemProdutosAsync(int id) =>
        context.Produtos.AnyAsync(p => p.ModoDisponibilizacaoId == id);

    public async Task<bool> AddAsync(ModoDisponibilizacao modo)
    {
        context.ModosDisponibilizacao.Add(modo);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(ModoDisponibilizacao modo)
    {
        context.ModosDisponibilizacao.Update(modo);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(ModoDisponibilizacao modo)
    {
        context.ModosDisponibilizacao.Remove(modo);
        return await context.SaveChangesAsync() > 0;
    }
}