using Microsoft.AspNetCore.Mvc;
using MyHotels.API.DTOs;
using MyHotels.API.Services;

namespace MyHotels.API.Controllers;

[Route("api/categorias")] // O endereço base para este recurso[cite: 46]
public class CategoriasController(ICategoriaService service) : ApiControllerBase
{
    /// <summary>Devolve a lista de todas as categorias, ordenadas pela Ordem.</summary>
    [HttpGet]
    public async Task<ActionResult<List<CategoriaDto>>> GetAll()
    {
        var lista = await service.GetAllAsync();
        return Ok(lista); // 200 OK[cite: 48]
    }

    /// <summary>Devolve uma categoria pelo seu Id.</summary>
    [HttpGet("{id:int}")] // O int garante que rotas inválidas como /api/categorias/abc dão logo 404[cite: 58]
    public async Task<ActionResult<CategoriaDto>> GetById(int id)
    {
        var r = await service.GetByIdAsync(id);
        if (!r.Sucesso) return Erro(r); // 404[cite: 46]
        return Ok(r.Valor);
    }

    /// <summary>Cria uma nova categoria.</summary>
    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Create(CriarCategoriaDto dto)
    {
        var r = await service.CreateAsync(dto);
        if (!r.Sucesso) return Erro(r); // 400 ou 409[cite: 48]

        // 201 Created: diz onde ficou o novo recurso (Header Location)[cite: 29, 48]
        return CreatedAtAction(nameof(GetById), new { id = r.Valor!.Id }, r.Valor);
    }

    /// <summary>Altera uma categoria existente.</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AtualizarCategoriaDto dto)
    {
        var r = await service.UpdateAsync(id, dto);
        if (!r.Sucesso) return Erro(r);
        return NoContent(); // 204 No Content: Correu bem, mas não há body para devolver[cite: 29, 48]
    }

    /// <summary>Apaga uma categoria se não tiver alojamentos associados.</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await service.DeleteAsync(id);
        if (!r.Sucesso) return Erro(r);
        return NoContent();
    }
}