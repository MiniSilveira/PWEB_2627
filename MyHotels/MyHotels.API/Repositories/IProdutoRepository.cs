using MyHotels.API.Entities;

namespace MyHotels.API.Repositories;

public interface IProdutoRepository
{
    // A listagem pode receber filtros da App (ex: produtos de uma categoria)[cite: 56]
    Task<List<Produto>> GetAllAsync(int? categoriaId, string? pesquisa);
    Task<Produto?> GetByIdAsync(int id);
    Task<bool> NomeExisteAsync(string nome, int? excetoId = null);
    Task<bool> AddAsync(Produto produto);
    Task<bool> UpdateAsync(Produto produto);
    Task<bool> DeleteAsync(Produto produto);
}