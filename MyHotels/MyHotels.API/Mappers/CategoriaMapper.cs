using MyHotels.API.DTOs;
using MyHotels.API.Entities;

namespace MyHotels.API.Mappers;

public static class CategoriaMapper
{
    // De Entidade para DTO de saída (GET)
    public static CategoriaDto ToDto(this Categoria c) => new()
    {
        Id = c.Id,
        Nome = c.Nome!,
        Ordem = c.Ordem,
        Imagem = c.Imagem // O .NET converte estes bytes em Base64 no JSON automaticamente
    };

    // De DTO de entrada para nova Entidade (POST)
    public static Categoria ToEntity(this CriarCategoriaDto d) => new()
    {
        Nome = d.Nome,
        Ordem = d.Ordem,
        Imagem = d.Imagem
    };

    // De DTO para atualizar uma Entidade existente (PUT)
    public static void AplicarEm(this AtualizarCategoriaDto d, Categoria c)
    {
        c.Nome = d.Nome;
        c.Ordem = d.Ordem;
        c.Imagem = d.Imagem;
    }
}