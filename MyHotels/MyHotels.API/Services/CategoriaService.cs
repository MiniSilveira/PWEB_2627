using MyHotels.API.DTOs;
using MyHotels.API.Erros;
using MyHotels.API.Mappers;
using MyHotels.API.Repositories;

namespace MyHotels.API.Services;

public class CategoriaService(ICategoriaRepository repository) : ICategoriaService
{
    public async Task<List<CategoriaDto>> GetAllAsync()
    {
        var categorias = await repository.GetAllAsync();
        return categorias.Select(c => c.ToDto()).ToList(); // Usa o ToDto do Mapper[cite: 52]
    }

    public async Task<Resultado<CategoriaDto>> GetByIdAsync(int id)
    {
        var categoria = await repository.GetByIdAsync(id);
        if (categoria == null)
            return Resultado<CategoriaDto>.NaoEncontrado(CodigosErro.CategoriaNaoEncontrada, $"A categoria com id {id} não existe.");

        return Resultado<CategoriaDto>.Ok(categoria.ToDto());
    }

    public async Task<Resultado<CategoriaDto>> CreateAsync(CriarCategoriaDto dto)
    {
        if (await repository.NomeExisteAsync(dto.Nome))
            return Resultado<CategoriaDto>.Conflito(CodigosErro.CategoriaNomeRepetido, $"Já existe uma categoria chamada '{dto.Nome}'.");

        if (dto.Imagem != null)
        {
            if (dto.Imagem.Length > Imagens.TamanhoMaximo)
                return Resultado<CategoriaDto>.Invalido(CodigosErro.ImagemDemasiadoGrande, "A imagem não pode ter mais de 2MB.");
            if (!Imagens.FormatoValido(dto.Imagem))
                return Resultado<CategoriaDto>.Invalido(CodigosErro.ImagemInvalida, "O formato da imagem é inválido. Use PNG, JPEG, GIF ou WebP.");
        }

        var categoria = dto.ToEntity();
        var gravou = await repository.AddAsync(categoria);

        if (!gravou)
            return Resultado<CategoriaDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        return Resultado<CategoriaDto>.Ok(categoria.ToDto());
    }

    public async Task<Resultado<CategoriaDto>> UpdateAsync(int id, AtualizarCategoriaDto dto)
    {
        var categoria = await repository.GetByIdAsync(id);
        if (categoria == null)
            return Resultado<CategoriaDto>.NaoEncontrado(CodigosErro.CategoriaNaoEncontrada, $"A categoria com id {id} não existe.");

        if (await repository.NomeExisteAsync(dto.Nome, id))
            return Resultado<CategoriaDto>.Conflito(CodigosErro.CategoriaNomeRepetido, $"Já existe outra categoria chamada '{dto.Nome}'.");

        if (dto.Imagem != null)
        {
            if (dto.Imagem.Length > Imagens.TamanhoMaximo)
                return Resultado<CategoriaDto>.Invalido(CodigosErro.ImagemDemasiadoGrande, "A imagem não pode ter mais de 2MB.");
            if (!Imagens.FormatoValido(dto.Imagem))
                return Resultado<CategoriaDto>.Invalido(CodigosErro.ImagemInvalida, "O formato da imagem é inválido. Use PNG, JPEG, GIF ou WebP.");
        }

        dto.AplicarEm(categoria);
        var gravou = await repository.UpdateAsync(categoria);

        if (!gravou)
            return Resultado<CategoriaDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        return Resultado<CategoriaDto>.Ok(categoria.ToDto());
    }

    public async Task<Resultado<CategoriaDto>> DeleteAsync(int id)
    {
        var categoria = await repository.GetByIdAsync(id);
        if (categoria == null)
            return Resultado<CategoriaDto>.NaoEncontrado(CodigosErro.CategoriaNaoEncontrada, $"A categoria com id {id} não existe.");

        // Regra de negócio: Impede que se apague se existirem produtos[cite: 73, 88]
        if (await repository.TemProdutosAsync(id))
            return Resultado<CategoriaDto>.Conflito(CodigosErro.CategoriaComProdutos, "Não é possível apagar a categoria porque tem alojamentos associados.");

        var gravou = await repository.DeleteAsync(categoria);

        if (!gravou)
            return Resultado<CategoriaDto>.Conflito(CodigosErro.GravacaoRecusada, "A base de dados recusou a gravação.");

        return Resultado<CategoriaDto>.Ok();
    }
}