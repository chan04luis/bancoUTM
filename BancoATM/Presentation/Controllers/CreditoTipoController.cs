using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CreditoTipoController : ControllerBase
{
    private readonly ICreditoTipoService _creditoTipoService;

    public CreditoTipoController(ICreditoTipoService creditoTipoService)
    {
        _creditoTipoService = creditoTipoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CreditoTipoDTO>>> GetAllCreditosTipo()
    {
        var creditosTipo = await _creditoTipoService.GetAllCreditosTipo();
        return Ok(creditosTipo);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditoTipoDTO>> GetCreditoTipoById(int id)
    {
        var creditoTipo = await _creditoTipoService.GetCreditoTipoById(id);
        if (creditoTipo == null)
        {
            return NotFound();
        }
        return Ok(creditoTipo);
    }

    [HttpPost]
    public async Task<ActionResult<CreditoTipoDTO>> AddCreditoTipo(CreditoTipoDTO creditoTipoDTO)
    {
        var addedCreditoTipo = await _creditoTipoService.AddCreditoTipo(creditoTipoDTO);
        return CreatedAtAction(nameof(GetCreditoTipoById), new { id = addedCreditoTipo.Id }, addedCreditoTipo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCreditoTipo(int id, CreditoTipoDTO creditoTipoDTO)
    {
        if (id != creditoTipoDTO.Id)
        {
            return BadRequest();
        }
        await _creditoTipoService.UpdateCreditoTipo(creditoTipoDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCreditoTipo(int id)
    {
        await _creditoTipoService.DeleteCreditoTipo(id);
        return NoContent();
    }
}
