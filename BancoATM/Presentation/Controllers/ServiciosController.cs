using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioService _servicioService;
        private readonly ITarjetaService _tarjetaService;
        private readonly IATMService _ATMService;
        private readonly ITransaccionService _transaccionService;
        public ServiciosController(IServicioService servicioService, ITarjetaService tarjetaService, IATMService aTMService, ITransaccionService transaccionService)
        {
            _servicioService = servicioService;
            _ATMService = aTMService;
            _tarjetaService = tarjetaService;
            _transaccionService = transaccionService;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            return Ok(await _servicioService.GetAllServicios());
        }
        [HttpPut]
        public async Task<IActionResult> applyPay([FromBody] ApplyPayServiceDTO apply)
        {
            var servicio = await _servicioService.GetServicioById(apply.PayId);
            if(servicio == null)
            {
                return Ok(new { success = false, message = "No existe el servicio" });
            }
            else
            {
                var tarjeta = await _tarjetaService.GetTarjetaById(apply.Id);
                if (tarjeta == null)
                {
                    return Ok(new { success = false, message = "No existe cliente" });
                }
                else
                {
                    decimal saldo = 0;
                    if (tarjeta.Tipo == 1)
                    {
                        saldo = tarjeta.Saldo;
                    }
                    else
                    {
                        saldo = tarjeta.Limite - tarjeta.Saldo;
                    }
                    if (saldo >= apply.Monto)
                    {
                        if (tarjeta.Tipo == 1)
                        {
                            tarjeta.Saldo -= apply.Monto;
                        }
                        else
                        {
                            tarjeta.Saldo += apply.Monto;
                        }
                        var destino = servicio.Tarjeta;
                        destino.Cliente = null;
                        if (destino.Tipo == 1)
                        {
                            destino.Saldo += apply.Monto;
                        }
                        else
                        {
                            destino.Saldo -= apply.Monto;
                        }
                        TransaccionDTO transaccion = new TransaccionDTO();
                        transaccion.Descripcion = $"Tranferencia de pago de {apply.Monto:F2} a servicio {servicio.Id} de {servicio.Nombre}, saldo actual {tarjeta.Saldo}";
                        transaccion.Id_Cuenta = tarjeta.Id;
                        transaccion.Id_Tipo = 2;
                        transaccion.Edo_cuenta = destino.Id;
                        transaccion.Referencia = apply.Referencia;
                        transaccion.Importe = apply.Monto;

                        TransaccionDTO transaccionDestino = new TransaccionDTO();
                        transaccionDestino.Descripcion = $"Pado de {apply.Monto:F2} a cuenta a tarjeta {destino.tarjeta} saldo actual {destino.Saldo}";
                        transaccionDestino.Id_Cuenta = destino.Id;
                        transaccionDestino.Id_Tipo = 6;
                        transaccionDestino.Edo_cuenta = tarjeta.Id;
                        transaccionDestino.Referencia = apply.Referencia;
                        transaccionDestino.Importe = apply.Monto;
                        var item1 = await _transaccionService.AddTransaccion(transaccion);
                        var item2 = await _transaccionService.AddTransaccion(transaccionDestino);
                        var service = await _tarjetaService.UpdateTarjeta(tarjeta);

                        return Ok(new { success = true, message = "Disponible", transaccion = item1, tarjeta = service });
                    }
                    else
                    {
                        return Ok(new { success = false, message = "No cuentas con el saldo para pagar" });
                    }
                }
            }
        }
    }
}
