using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TarjetaController : ControllerBase
{
    private readonly ITarjetaService _tarjetaService;
    public TarjetaController(ITarjetaService tarjetaService)
    {
        _tarjetaService = tarjetaService;
    }
    [HttpGet("read/{tarjeta}")]
    public async Task<IActionResult> ReadTarjeta(string tarjeta)
    {
        try
        {
            var result = await _tarjetaService.GetTarjetaByNip(tarjeta);
            if (result != null)
            {
                return Ok(new { success = true, message = "Cliente encontrado", nombre=result.Cliente.Nombres, apellido=result.Cliente.Apellidos, id=result.Id });
            }
            else
            {
                return Ok(new { success = false, message = "Cliente no encontrado" });
            }
        }catch(Exception ex)
        {
            return Ok(new { success=false,  message=ex.Message });
        }
    }
    [HttpGet("read/{id}/{nip}")]
    public async Task<IActionResult> ReadNip(int id, string nip)
    {
        try
        {
            var tarjeta = await _tarjetaService.GetTarjetaById(id);
            var resp = await _tarjetaService.validateNip(nip, id, true);
            if (!resp.Item1)
            {
                return Ok(new { success = false, message = resp.Item2 });
            }
            else
            {
                return Ok(new { success = true, item = tarjeta });
            }
        }
        catch (Exception ex)
        {
            return Ok(new { success = false, message = ex.Message });
        }
    }
    [HttpPut("changeNip")]
    public async Task<IActionResult> ChangeNip([FromBody] TarjetaChangeNipDTO tarjetaNip)
    {
        try
        {
            var tarjeta = await _tarjetaService.GetTarjetaById(tarjetaNip.id);
            var resp = await _tarjetaService.validateNip(tarjetaNip.nip, tarjetaNip.id, true);
            var resp_new = await _tarjetaService.validateNip(tarjetaNip.nip_nuevo, tarjetaNip.id, false);
            if (!resp.Item1)
            {
                return Ok(new { success = false, message = resp.Item2 });
            }
            else if(!resp_new.Item1)
            {
                return Ok(new { success = false, message = resp_new.Item2 });
            }
            else
            {
                tarjeta.Nip = tarjetaNip.nip_nuevo;
                var answer_change = await _tarjetaService.UpdateTarjeta(tarjeta);
                return Ok(new { success = true, message = answer_change });
            }
            return Ok(new { success = resp, message = resp_new, tarjeta= tarjeta });
        }
        catch(Exception ex)
        {
            return Ok(new { success = false, message = ex.Message });
        }
    }


    /*[HttpPost("Depositar/firts")]
    public async Task<IActionResult> firtsValidation()
    {

    }*/
}