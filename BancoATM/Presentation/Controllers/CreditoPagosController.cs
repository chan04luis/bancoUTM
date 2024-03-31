using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CreditoPagosController : ControllerBase
{
    private readonly ICreditoPagosService _creditoPagosService;

    public CreditoPagosController(ICreditoPagosService creditoPagosService)
    {
        _creditoPagosService = creditoPagosService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditoPagosDTO>> GetCreditoPagos(int id)
    {
        var creditoPagos = await _creditoPagosService.GetCreditoPagosById(id);
        if (creditoPagos == null)
        {
            return NotFound();
        }
        return creditoPagos;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCreditoPagos(int id, CreditoPagosDTO creditoPagosDTO)
    {
        if (id != creditoPagosDTO.Id)
        {
            return BadRequest();
        }

        try
        {
            var updatedCreditoPagos = await _creditoPagosService.UpdateCreditoPagos(creditoPagosDTO);
            return Ok(updatedCreditoPagos);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCreditoPagos(int id)
    {
        var result = await _creditoPagosService.DeleteCreditoPagos(id);
        if (result == 0)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("credito/{idCredito}")]
    public async Task<ActionResult<List<CreditoPagosDTO>>> GetCreditoPagosByCreditoId(int idCredito)
    {
        var creditoPagosList = await _creditoPagosService.GetCreditoPagosByCreditoId(idCredito);
        if (creditoPagosList == null || creditoPagosList.Count == 0)
        {
            return NotFound();
        }
        return creditoPagosList;
    }
}
