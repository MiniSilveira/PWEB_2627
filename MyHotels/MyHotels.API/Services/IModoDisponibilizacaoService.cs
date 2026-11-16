using MyHotels.API.DTOs;

namespace MyHotels.API.Services;

public interface IModoDisponibilizacaoService
{
    Task<List<ModoDisponibilizacaoDto>> GetAllAsync();
    Task<Resultado<ModoDisponibilizacaoDto>> GetByIdAsync(int id);
    Task<Resultado<ModoDisponibilizacaoDto>> CreateAsync(CriarModoDisponibilizacaoDto dto);
    Task<Resultado<ModoDisponibilizacaoDto>> UpdateAsync(int id, AtualizarModoDisponibilizacaoDto dto);
    Task<Resultado<ModoDisponibilizacaoDto>> DeleteAsync(int id);
}