using MyHotels.API.Entities;

namespace MyHotels.API.Repositories;

public interface ICategoriaRepository
{
    Task<List<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int id);
    Task<bool> NomeExisteAsync(string nome, int? excetoId = null);
    Task<bool> ExisteAsync(int id);
    Task<bool> TemProdutosAsync(int id);
    Task<bool> AddAsync(Categoria categoria);
    Task<bool> UpdateAsync(Categoria categoria);
    Task<bool> DeleteAsync(Categoria categoria);
}