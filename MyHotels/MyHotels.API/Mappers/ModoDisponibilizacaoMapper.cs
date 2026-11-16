using MyHotels.API.DTOs;
using MyHotels.API.Entities;

namespace MyHotels.API.Mappers;

public static class ModoDisponibilizacaoMapper
{
    public static ModoDisponibilizacaoDto ToDto(this ModoDisponibilizacao m) => new()
    {
        Id = m.Id,
        Nome = m.Nome!
    };

    public static ModoDisponibilizacao ToEntity(this CriarModoDisponibilizacaoDto d) => new()
    {
        Nome = d.Nome
    };

    public static void AplicarEm(this AtualizarModoDisponibilizacaoDto d, ModoDisponibilizacao m)
    {
        m.Nome = d.Nome;
    }
}