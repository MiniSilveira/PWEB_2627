using Microsoft.AspNetCore.Mvc;
using MyHotels.API.DTOs;
using MyHotels.API.Services;

namespace MyHotels.API.Controllers;

[Route("api/modos-disponibilizacao")] // Regra REST: plural, minúsculas e separado por hífen[cite: 25]
public class ModosDisponibilizacaoController(IModoDisponibilizacaoService service) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ModoDisponibilizacaoDto>>> GetAll()
    {
        var lista = await service.GetAllAsync();
        return Ok(lista);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ModoDisponibilizacaoDto>> GetById(int id)
    {
        var r = await service.GetByIdAsync(id);
        if (!r.Sucesso) return Erro(r);
        return Ok(r.Valor);
    }

    [HttpPost]
    public async Task<ActionResult<ModoDisponibilizacaoDto>> Create(CriarModoDisponibilizacaoDto dto)
    {
        var r = await service.CreateAsync(dto);
        if (!r.Sucesso) return Erro(r);
        return CreatedAtAction(nameof(GetById), new { id = r.Valor!.Id }, r.Valor);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AtualizarModoDisponibilizacaoDto dto)
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