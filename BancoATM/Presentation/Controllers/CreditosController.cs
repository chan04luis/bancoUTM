using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

[Route("api/[controller]")]
[ApiController]
public class CreditosController : ControllerBase
{
    private readonly ICreditoService _creditoService;
    private readonly ICreditoTipoService _tipoCreditoService;

    public CreditosController(ICreditoService creditoService, ICreditoTipoService tipoCreditoService)
    {
        _creditoService = creditoService;
        _tipoCreditoService = tipoCreditoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CreditoDTO>>> GetAllCreditos()
    {
        var creditos = await _creditoService.GetAllCreditos();
        return creditos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCredito(int id)
    {
        var credito = await _creditoService.GetCreditoById(id);
        if (credito == null)
        {
            return NotFound();
        }
        return Ok(credito);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCredito(CreditoDTO creditoDTO)
    {
        var newCredito = await _creditoService.CreateCredito(creditoDTO, creditoDTO.creditoPagosDTO);
        return Ok(CreatedAtAction(nameof(GetCredito), new { id = newCredito.Id }, newCredito));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCredito(int id, CreditoDTO creditoDTO)
    {
        if (id != creditoDTO.Id)
        {
            return BadRequest();
        }

        try
        {
            var updatedCredito = await _creditoService.UpdateCredito(creditoDTO);
            return Ok(updatedCredito);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCredito(int id)
    {
        var result = await _creditoService.DeleteCredito(id);
        if (result == 0)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("calcularAmortizacion")]
    public async Task<ActionResult> CalcularAmortizacion(CalculoAmortizacionDTO calculo)
    {
        var tipoCredito = await _tipoCreditoService.GetCreditoTipoById(calculo.IdTipoCredito);
        if (tipoCredito == null)
        {
            return NotFound("El tipo de crédito especificado no existe.");
        }
        var tasaInteres = tipoCredito.TasaAnual / 12; 
        var valorPago = Financial.Pmt((double)tasaInteres, calculo.PlazoMeses, (double)-calculo.MontoCredito);
        var nuevoCredito = new CreditoDTO
        {
            Estatus = 1,
            IdTipoCredito = calculo.IdTipoCredito,
            PlazoMeses = calculo.PlazoMeses,
            TasaInteres = tipoCredito.TasaAnual,
            CreditoOtorgado = calculo.MontoCredito,
            FechaInicio = calculo.FechaInicio,
            FechaFinal = calculo.FechaInicio.AddMonths(calculo.PlazoMeses),
            Pago = (decimal)valorPago,
            FechaRegistro = DateTime.Now,
            FechaActualizado = DateTime.Now
        };

        var creditoPagosList = new List<CreditoPagosDTO>();
        decimal capitalPendiente = calculo.MontoCredito;
        for (int i = 1; i <= calculo.PlazoMeses; i++)
        {
            var interesPendiente = capitalPendiente * decimal.Parse(tasaInteres.ToString());
            var capitalPago = decimal.Parse(valorPago.ToString()) - interesPendiente;
            var pago = new CreditoPagosDTO
            {
                NumPago = i,
                Pago = decimal.Parse(valorPago.ToString()),
                Fecha = calculo.FechaInicio.AddMonths(i),
                FechaPago = null,
                Capital = capitalPago,
                Interes = interesPendiente,
                Estatus = 0,
                IdCredito = 0, 
                FechaRegistro = DateTime.Now,
                FechaActualizado = DateTime.Now
            };
            capitalPendiente -= capitalPago;
            creditoPagosList.Add(pago);
        }
        nuevoCredito.creditoPagosDTO = creditoPagosList;
        return Ok(nuevoCredito);
    }

}
