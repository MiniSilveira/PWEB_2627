using Microsoft.EntityFrameworkCore;
using MyHotels.API.Data;
using MyHotels.API.Entities;

namespace MyHotels.API.Repositories;

public class CategoriaRepository(AppDbContext context) : ICategoriaRepository
{
    // AsNoTracking() é usado nas listagens porque apenas queremos ler os dados, tornando a query mais leve[cite: 56, 87]
    public Task<List<Categoria>> GetAllAsync() =>
        context.Categorias.OrderBy(c => c.Ordem).AsNoTracking().ToListAsync();

    public Task<Categoria?> GetByIdAsync(int id) =>
        context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

    public Task<bool> NomeExisteAsync(string nome, int? excetoId = null) =>
        context.Categorias.AnyAsync(c => c.Nome == nome && c.Id != excetoId);

    public Task<bool> ExisteAsync(int id) =>
        context.Categorias.AnyAsync(c => c.Id == id);

    // Confirma se a categoria tem produtos associados para evitar apagá-la[cite: 74]
    public Task<bool> TemProdutosAsync(int id) =>
        context.Produtos.AnyAsync(p => p.CategoriaId == id);

    // As gravações devolvem bool (true se gravou linhas, false se a base de dados recusou)
    public async Task<bool> AddAsync(Categoria categoria)
    {
        context.Categorias.Add(categoria);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Categoria categoria)
    {
        context.Categorias.Update(categoria);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Categoria categoria)
    {
        context.Categorias.Remove(categoria);
        return await context.SaveChangesAsync() > 0;
    }
}