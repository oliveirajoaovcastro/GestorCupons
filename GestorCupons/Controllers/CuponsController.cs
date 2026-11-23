using GestorCupons.DTOs;
using GestorCupons.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorCupons.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuponsController : ControllerBase
{
    private readonly ICupomService _cupomService;

    public CuponsController(ICupomService cupomService)
    {
        _cupomService = cupomService;
    }

    [HttpPost]
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
}