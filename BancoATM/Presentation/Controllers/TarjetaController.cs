using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TarjetaController : ControllerBase
{
    private readonly ITarjetaService _tarjetaService;

    private readonly ITransaccionService _transaccionService;
    public TarjetaController(ITarjetaService tarjetaService, ITransaccionService transaccionService)
    {
        _tarjetaService = tarjetaService;
        _transaccionService = transaccionService;
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
                tarjeta.Fecha_Actualizado = DateTime.Now;
                TransaccionDTO transaccion = new TransaccionDTO();
                transaccion.Descripcion = "Cambio de nip";
                transaccion.Id_Cuenta = tarjeta.Id;
                transaccion.Id_Tipo = 5;
                transaccion.Edo_cuenta = tarjeta.Id;
                transaccion.Referencia = "";
                transaccion.Importe = 0;
                var item = await _transaccionService.AddTransaccion(transaccion);
                var answer_change = await _tarjetaService.UpdateTarjeta(tarjeta);

                return Ok(new { success = true, item = answer_change, transaccion= item });
            }
        }
        catch(Exception ex)
        {
            return Ok(new { success = false, message = ex.Message });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> getSingle(int id)
    {
        return Ok(await _tarjetaService.GetTarjetaById(id));
    }
    [HttpGet("last5/{id}")]
    public async Task<IActionResult> get5(int id)
    {
        var items =await _transaccionService.GetAllTransacciones();
        var results = items
            .Where(x=>x.Id_Cuenta==id && x.TipoTransaccion.Edo_Cuenta==1)
            .OrderByDescending(x=>x.Fecha_Registro)
            .Take(5)
            .ToList();
        return Ok(results);
    }
    [HttpGet("today/{id}")]
    public async Task<IActionResult> getToday(int id)
    {
        var items = await _transaccionService.GetAllTransacciones();
        var today = DateTime.Today;
        var results = items
            .Where(x => x.Id_Cuenta == id 
            && x.TipoTransaccion.Edo_Cuenta == 1
            && x.Id_Tipo==2
            && x.Fecha_Registro.Date == today)
            .ToList();
        return Ok(new { total = results.Sum(x=>x.Importe), fecha= DateTime.Now.Date });
    }

    [HttpPut("retirar")]
    public async Task<IActionResult> withdraw([FromBody] TarjetaRetiroDTO tarjetaRetiro)
    {
        try
        {
            var tarjeta = await _tarjetaService.GetTarjetaById(tarjetaRetiro.id);
            tarjeta.Saldo -= tarjetaRetiro.retiro;
            tarjeta.Fecha_Actualizado = DateTime.Now;
            TransaccionDTO transaccion = new TransaccionDTO();
            transaccion.Descripcion = $"Retiro de {tarjetaRetiro.retiro:F2} a cuenta a tarjeta {tarjeta.Tarjeta} saldo actual {tarjeta.Saldo}";
            transaccion.Id_Cuenta = tarjeta.Id;
            transaccion.Id_Tipo = 2;
            transaccion.Edo_cuenta = tarjeta.Id;
            transaccion.Referencia = "";
            transaccion.Importe = 0;
            var item = await _transaccionService.AddTransaccion(transaccion);
            var answer_change = await _tarjetaService.UpdateTarjeta(tarjeta);

            return Ok(new { success = true, item = answer_change, transaccion = item });
        }
        catch (Exception ex)
        {
            return Ok(new { success = false, message = ex.Message });
        }
    }
}