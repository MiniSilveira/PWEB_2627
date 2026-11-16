using MyHotels.API.DTOs;
using MyHotels.API.Erros;
using MyHotels.API.Mappers;
using MyHotels.API.Repositories;

namespace MyHotels.API.Services;

public class ModoDisponibilizacaoService(IModoDisponibilizacaoRepository repository) : IModoDisponibilizacaoService
{
    public async Task<List<ModoDisponibilizacaoDto>> GetAllAsync()
    {
        var modos = await repository.GetAllAsync();
        return modos.Select(m => m.ToDto()).ToList();
    }

    public async Task<Resultado<ModoDisponibilizacaoDto>> GetByIdAsync(int id)
    {
        var modo = await repository.GetByIdAsync(id);
        if (modo == null)
            return Resultado<ModoDisponibilizacaoDto>.NaoEncontrado(CodigosErro.ModoNaoEncontrado, $"O modo de disponibilização com id {id} não existe.");

        return Resultado<ModoDisponibilizacaoDto>.Ok(modo.ToDto());
    }

    public async Task<Resultado<ModoDisponibilizacaoDto>> CreateAsync(CriarModoDisponibilizacaoDto dto)
    {
        if (await repository.NomeExisteAsync(dto.Nome))
            return Resultado<ModoDisponibilizacaoDto>.Conflito(CodigosErro.ModoNomeRepetido, $"Já existe um modo chamado '{dto.Nome}'.");

        var modo = dto.ToEntity();
        var gravou = await repository.AddAsync(modo);

        if (!gravou)
            return Resultado<ModoDisponibilizacaoDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        return Resultado<ModoDisponibilizacaoDto>.Ok(modo.ToDto());
    }

    public async Task<Resultado<ModoDisponibilizacaoDto>> UpdateAsync(int id, AtualizarModoDisponibilizacaoDto dto)
    {
        var modo = await repository.GetByIdAsync(id);
        if (modo == null)
            return Resultado<ModoDisponibilizacaoDto>.NaoEncontrado(CodigosErro.ModoNaoEncontrado, $"O modo com id {id} não existe.");

        if (await repository.NomeExisteAsync(dto.Nome, id))
            return Resultado<ModoDisponibilizacaoDto>.Conflito(CodigosErro.ModoNomeRepetido, $"Já existe outro modo chamado '{dto.Nome}'.");

        dto.AplicarEm(modo);
        var gravou = await repository.UpdateAsync(modo);

        if (!gravou)
            return Resultado<ModoDisponibilizacaoDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        return Resultado<ModoDisponibilizacaoDto>.Ok(modo.ToDto());
    }

    public async Task<Resultado<ModoDisponibilizacaoDto>> DeleteAsync(int id)
    {
        var modo = await repository.GetByIdAsync(id);
        if (modo == null)
            return Resultado<ModoDisponibilizacaoDto>.NaoEncontrado(CodigosErro.ModoNaoEncontrado, $"O modo com id {id} não existe.");

        if (await repository.TemProdutosAsync(id))
            return Resultado<ModoDisponibilizacaoDto>.Conflito(CodigosErro.ModoEmUso, "Não é possível apagar o modo de disponibilização porque tem alojamentos associados.");

        var gravou = await repository.DeleteAsync(modo);

        if (!gravou)
            return Resultado<ModoDisponibilizacaoDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        return Resultado<ModoDisponibilizacaoDto>.Ok();
    }
}