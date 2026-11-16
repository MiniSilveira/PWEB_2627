using MyHotels.API.DTOs;

namespace MyHotels.API.Services;

public interface ICategoriaService
{
    Task<List<CategoriaDto>> GetAllAsync();
    Task<Resultado<CategoriaDto>> GetByIdAsync(int id);
    Task<Resultado<CategoriaDto>> CreateAsync(CriarCategoriaDto dto);
    Task<Resultado<CategoriaDto>> UpdateAsync(int id, AtualizarCategoriaDto dto);
    Task<Resultado<CategoriaDto>> DeleteAsync(int id);
}