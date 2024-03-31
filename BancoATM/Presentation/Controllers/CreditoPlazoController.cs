using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CreditoPlazoController : ControllerBase
{
    private readonly ICreditoPlazoService _creditoPlazoService;

    public CreditoPlazoController(ICreditoPlazoService creditoPlazoService)
    {
        _creditoPlazoService = creditoPlazoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CreditoPlazoDTO>>> GetAllCreditosPlazo()
    {
        var creditosPlazo = await _creditoPlazoService.GetAllCreditosPlazo();
        return Ok(creditosPlazo);
    }
    [HttpGet("ByTipo/{id}")]
    public async Task<ActionResult<List<CreditoPlazoDTO>>> GetAllCreditosPlazoByTipo(int id)
    {
        var creditosPlazo = await _creditoPlazoService.GetAllCreditosPlazoByTipo(id);
        return Ok(creditosPlazo);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditoPlazoDTO>> GetCreditoPlazoById(int id)
    {
        var creditoPlazo = await _creditoPlazoService.GetCreditoPlazoById(id);
        if (creditoPlazo == null)
        {
            return NotFound();
        }
        return Ok(creditoPlazo);
    }

    [HttpPost]
    public async Task<ActionResult<CreditoPlazoDTO>> AddCreditoPlazo(CreditoPlazoDTO creditoPlazoDTO)
    {
        var addedCreditoPlazo = await _creditoPlazoService.AddCreditoPlazo(creditoPlazoDTO);
        return CreatedAtAction(nameof(GetCreditoPlazoById), new { id = addedCreditoPlazo.Id }, addedCreditoPlazo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCreditoPlazo(int id, CreditoPlazoDTO creditoPlazoDTO)
    {
        if (id != creditoPlazoDTO.Id)
        {
            return BadRequest();
        }
        await _creditoPlazoService.UpdateCreditoPlazo(creditoPlazoDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCreditoPlazo(int id)
    {
        await _creditoPlazoService.DeleteCreditoPlazo(id);
        return NoContent();
    }
}
