using MyHotels.API.DTOs;
using MyHotels.API.Erros;
using MyHotels.API.Mappers;
using MyHotels.API.Repositories;

namespace MyHotels.API.Services;

public class ProdutoService(
    IProdutoRepository produtos,
    ICategoriaRepository categorias,
    IModoDisponibilizacaoRepository modos) : IProdutoService
{
    public async Task<List<ProdutoDto>> GetAllAsync(int? categoriaId, string? pesquisa)
    {
        var lista = await produtos.GetAllAsync(categoriaId, pesquisa);
        return lista.Select(p => p.ToDto()).ToList();
    }

    public async Task<Resultado<ProdutoDto>> GetByIdAsync(int id)
    {
        var produto = await produtos.GetByIdAsync(id);
        if (produto == null)
            return Resultado<ProdutoDto>.NaoEncontrado(CodigosErro.ProdutoNaoEncontrado, $"O alojamento com id {id} não existe.");

        return Resultado<ProdutoDto>.Ok(produto.ToDto());
    }

    public async Task<Resultado<ProdutoDto>> CreateAsync(CriarProdutoDto dto)
    {
        // Regras de negócio cruzadas com outras tabelas[cite: 54, 71]
        if (!await categorias.ExisteAsync(dto.CategoriaId))
            return Resultado<ProdutoDto>.Invalido(CodigosErro.CategoriaNaoEncontrada, "A categoria indicada não existe.");

        if (dto.ModoDisponibilizacaoId.HasValue && !await modos.ExisteAsync(dto.ModoDisponibilizacaoId.Value))
            return Resultado<ProdutoDto>.Invalido(CodigosErro.ModoNaoEncontrado, "O modo de disponibilização indicado não existe.");

        if (await produtos.NomeExisteAsync(dto.Nome))
            return Resultado<ProdutoDto>.Conflito(CodigosErro.ProdutoNomeRepetido, $"Já existe um alojamento chamado '{dto.Nome}'.");

        if (dto.Imagem != null)
        {
            if (dto.Imagem.Length > Imagens.TamanhoMaximo)
                return Resultado<ProdutoDto>.Invalido(CodigosErro.ImagemDemasiadoGrande, "A imagem não pode ter mais de 2MB.");
            if (!Imagens.FormatoValido(dto.Imagem))
                return Resultado<ProdutoDto>.Invalido(CodigosErro.ImagemInvalida, "O formato da imagem é inválido. Use PNG, JPEG, GIF ou WebP.");
        }

        var produto = dto.ToEntity();
        var gravou = await produtos.AddAsync(produto);

        if (!gravou)
            return Resultado<ProdutoDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        // O EF Core preencheu o Id[cite: 72]. Lemos de novo para vir com as propriedades Categoria e Modo preenchidas (Include) para o DTO.
        var criado = await produtos.GetByIdAsync(produto.Id);
        return Resultado<ProdutoDto>.Ok(criado!.ToDto());
    }

    public async Task<Resultado<ProdutoDto>> UpdateAsync(int id, AtualizarProdutoDto dto)
    {
        var produto = await produtos.GetByIdAsync(id);
        if (produto == null)
            return Resultado<ProdutoDto>.NaoEncontrado(CodigosErro.ProdutoNaoEncontrado, $"O alojamento com id {id} não existe.");

        if (!await categorias.ExisteAsync(dto.CategoriaId))
            return Resultado<ProdutoDto>.Invalido(CodigosErro.CategoriaNaoEncontrada, "A categoria indicada não existe.");

        if (dto.ModoDisponibilizacaoId.HasValue && !await modos.ExisteAsync(dto.ModoDisponibilizacaoId.Value))
            return Resultado<ProdutoDto>.Invalido(CodigosErro.ModoNaoEncontrado, "O modo de disponibilização indicado não existe.");

        if (await produtos.NomeExisteAsync(dto.Nome, id))
            return Resultado<ProdutoDto>.Conflito(CodigosErro.ProdutoNomeRepetido, $"Já existe outro alojamento chamado '{dto.Nome}'.");

        if (dto.Imagem != null)
        {
            if (dto.Imagem.Length > Imagens.TamanhoMaximo)
                return Resultado<ProdutoDto>.Invalido(CodigosErro.ImagemDemasiadoGrande, "A imagem não pode ter mais de 2MB.");
            if (!Imagens.FormatoValido(dto.Imagem))
                return Resultado<ProdutoDto>.Invalido(CodigosErro.ImagemInvalida, "O formato da imagem é inválido. Use PNG, JPEG, GIF ou WebP.");
        }

        dto.AplicarEm(produto);
        var gravou = await produtos.UpdateAsync(produto);

        if (!gravou)
            return Resultado<ProdutoDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        var atualizado = await produtos.GetByIdAsync(produto.Id);
        return Resultado<ProdutoDto>.Ok(atualizado!.ToDto());
    }

    public async Task<Resultado<ProdutoDto>> DeleteAsync(int id)
    {
        var produto = await produtos.GetByIdAsync(id);
        if (produto == null)
            return Resultado<ProdutoDto>.NaoEncontrado(CodigosErro.ProdutoNaoEncontrado, $"O alojamento com id {id} não existe.");

        var gravou = await produtos.DeleteAsync(produto);

        if (!gravou)
            return Resultado<ProdutoDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        return Resultado<ProdutoDto>.Ok();
    }
}