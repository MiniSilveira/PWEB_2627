using MyHotels.API.DTOs;

namespace MyHotels.API.Services;

public interface IProdutoService
{
    Task<List<ProdutoDto>> GetAllAsync(int? categoriaId, string? pesquisa);
    Task<Resultado<ProdutoDto>> GetByIdAsync(int id);
    Task<Resultado<ProdutoDto>> CreateAsync(CriarProdutoDto dto);
    Task<Resultado<ProdutoDto>> UpdateAsync(int id, AtualizarProdutoDto dto);
    Task<Resultado<ProdutoDto>> DeleteAsync(int id);
}