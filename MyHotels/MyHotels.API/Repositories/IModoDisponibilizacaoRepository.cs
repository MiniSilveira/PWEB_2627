using MyHotels.API.Entities;

namespace MyHotels.API.Repositories;

public interface IModoDisponibilizacaoRepository
{
    Task<List<ModoDisponibilizacao>> GetAllAsync();
    Task<ModoDisponibilizacao?> GetByIdAsync(int id);
    Task<bool> NomeExisteAsync(string nome, int? excetoId = null);
    Task<bool> ExisteAsync(int id);
    Task<bool> TemProdutosAsync(int id);
    Task<bool> AddAsync(ModoDisponibilizacao modo);
    Task<bool> UpdateAsync(ModoDisponibilizacao modo);
    Task<bool> DeleteAsync(ModoDisponibilizacao modo);
}