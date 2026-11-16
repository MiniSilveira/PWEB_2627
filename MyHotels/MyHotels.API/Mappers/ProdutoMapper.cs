using MyHotels.API.DTOs;
using MyHotels.API.Entities;

namespace MyHotels.API.Mappers;

public static class ProdutoMapper
{
    public static ProdutoDto ToDto(this Produto p) => new()
    {
        Id = p.Id,
        Nome = p.Nome!,
        Detalhe = p.Detalhe,
        PrecoBase = p.PrecoBase,
        Disponibilidade = p.Disponibilidade,
        Visivel = p.Visivel,

        // Converte as classes complexas num DTO simples com Id e Nome
        Categoria = new ReferenciaDto { Id = p.Categoria!.Id, Nome = p.Categoria.Nome! },
        ModoDisponibilizacao = p.ModoDisponibilizacao != null
            ? new ReferenciaDto { Id = p.ModoDisponibilizacao.Id, Nome = p.ModoDisponibilizacao.Nome! }
            : null,

        Imagem = p.Imagem
    };

    public static Produto ToEntity(this CriarProdutoDto d) => new()
    {
        Nome = d.Nome,
        Detalhe = d.Detalhe,
        PrecoBase = d.PrecoBase,
        Disponibilidade = d.Disponibilidade,
        Visivel = d.Visivel,
        CategoriaId = d.CategoriaId,
        ModoDisponibilizacaoId = d.ModoDisponibilizacaoId,
        Imagem = d.Imagem
    };

    public static void AplicarEm(this AtualizarProdutoDto d, Produto p)
    {
        p.Nome = d.Nome;
        p.Detalhe = d.Detalhe;
        p.PrecoBase = d.PrecoBase;
        p.Disponibilidade = d.Disponibilidade;
        p.Visivel = d.Visivel;
        p.CategoriaId = d.CategoriaId;
        p.ModoDisponibilizacaoId = d.ModoDisponibilizacaoId;
        p.Imagem = d.Imagem;
    }
}