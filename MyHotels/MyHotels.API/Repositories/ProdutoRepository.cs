using Microsoft.EntityFrameworkCore;
using MyHotels.API.Data;
using MyHotels.API.Entities;

namespace MyHotels.API.Repositories;

public class ProdutoRepository(AppDbContext context) : IProdutoRepository
{
    public Task<List<Produto>> GetAllAsync(int? categoriaId, string? pesquisa)
    {
        // O Include traz a categoria e o modo associados num único SELECT com JOIN[cite: 56, 87]
        var query = context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.ModoDisponibilizacao)
            .AsNoTracking()
            .AsQueryable();

        if (categoriaId.HasValue)
            query = query.Where(p => p.CategoriaId == categoriaId.Value);

        if (!string.IsNullOrEmpty(pesquisa))
            query = query.Where(p => p.Nome!.Contains(pesquisa));

        return query.ToListAsync();
    }

    public Task<Produto?> GetByIdAsync(int id) =>
        context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.ModoDisponibilizacao)
            .FirstOrDefaultAsync(p => p.Id == id); // Traz as relações para o GET[cite: 56]

    public Task<bool> NomeExisteAsync(string nome, int? excetoId = null) =>
        context.Produtos.AnyAsync(p => p.Nome == nome && p.Id != excetoId);

    public async Task<bool> AddAsync(Produto produto)
    {
        context.Produtos.Add(produto);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Produto produto)
    {
        context.Produtos.Update(produto);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Produto produto)
    {
        context.Produtos.Remove(produto);
        return await context.SaveChangesAsync() > 0;
    }
}