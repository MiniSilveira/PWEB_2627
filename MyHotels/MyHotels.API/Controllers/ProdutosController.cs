using Microsoft.AspNetCore.Mvc;
using MyHotels.API.DTOs;
using MyHotels.API.Services;

namespace MyHotels.API.Controllers;

[Route("api/produtos")]
public class ProdutosController(IProdutoService service) : ApiControllerBase
{
    /// <summary>Lista de alojamentos. Pode ser filtrada por categoria ou texto.</summary>
    [HttpGet]
    public async Task<ActionResult<List<ProdutoDto>>> GetAll(
        [FromQuery] int? categoriaId, // O [FromQuery] tira o valor de ?categoriaId=1 no URL[cite: 58]
        [FromQuery] string? pesquisa)
    {
        var lista = await service.GetAllAsync(categoriaId, pesquisa);
        return Ok(lista);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> GetById(int id)
    {
        var r = await service.GetByIdAsync(id);
        if (!r.Sucesso) return Erro(r);
        return Ok(r.Valor);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create(CriarProdutoDto dto)
    {
        var r = await service.CreateAsync(dto);
        if (!r.Sucesso) return Erro(r);
        return CreatedAtAction(nameof(GetById), new { id = r.Valor!.Id }, r.Valor);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AtualizarProdutoDto dto)
    {
        var r = await service.UpdateAsync(id, dto);
        if (!r.Sucesso) return Erro(r);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await service.DeleteAsync(id);
        if (!r.Sucesso) return Erro(r);
        return NoContent();
    }
}