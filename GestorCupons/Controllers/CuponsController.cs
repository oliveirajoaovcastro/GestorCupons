using GestorCupons.DTOs;
using GestorCupons.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorCupons.Controllers;

[ApiController]
[Route("api/GestorCupons/Cupons")]
public class CuponsController : ControllerBase
{
    private readonly ICupomService _cupomService;

    public CuponsController(ICupomService cupomService)
    {
        _cupomService = cupomService;
    }

    [HttpPost("criar-cupom")]
    public IActionResult CriarCupom([FromBody] CriarCupomRequest request)
    {
        try
        {
            var cupomCriado = _cupomService.CriarCupom(request);
            return StatusCode(201, cupomCriado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
    
    [HttpGet("listar-cupons")]
    public IActionResult ListarCupons()
    {
        var cupons = _cupomService.ListarCupons();
        return Ok(cupons);
    }

    [HttpGet("obter-cupom/{id}")]
    public IActionResult ObterPorId(int id)
    {
        var cupom = _cupomService.ObterPorId(id);

        if (cupom is null)
            return NotFound(new { erro = $"Cupom com ID {id} não encontrado." });

        return Ok(cupom);
    }

    [HttpPut("atualizar-cupom/{id}")]
    public IActionResult AtualizarCupom(int id, [FromBody] AtualizarCupomRequest request)
    {
        try
        {
            var cupom = _cupomService.AtualizarCupom(id, request);

            if (cupom is null)
                return NotFound(new { erro = $"Cupom com ID {id} não encontrado." });

            return Ok(cupom);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete("deletar-cupom/{id}")]
    public IActionResult DeletarCupom(int id)
    {
        var deletado = _cupomService.DeletarCupom(id);

        if (!deletado)
            return NotFound(new { erro = $"Cupom com ID {id} não encontrado." });

        return NoContent();
    }
}