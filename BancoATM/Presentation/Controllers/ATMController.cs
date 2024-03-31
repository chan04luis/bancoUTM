using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        private readonly IATMService _ATMService;
        private readonly ITarjetaService _tarjetaService;
        public ATMController(IATMService aTMService, ITarjetaService tarjetaService)
        {
            _ATMService = aTMService;
            _tarjetaService = tarjetaService;
        }
        [HttpGet("{tipo}")]
        public async Task<IActionResult> validateRetiro(int tipo=1)
        {
            try
            {
                var items = await _ATMService.GetAllATMs();
                if(tipo!=0)
                {
                    items = items.Where(x=> x.Tipo == tipo).ToList();
                }
                return Ok(new { 
                    success = true, 
                    notAvailable = items.Where(x=> x.Cantidad==0).ToList(), 
                    free=items.Where(x=>x.Cantidad> 0),
                    freeCash= items.Where(x => x.Cantidad > 0).Sum(x=>x.Cantidad*x.Denominacion)
                });
            }catch(Exception ex)
            {
                return Ok(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [HttpGet("available/{id}/{cash}")]
        public async Task<IActionResult> descargarSaldo(int id, int cash, int type=0)
        {
            try
            {
                var items = await _ATMService.GetAllATMs();
                items = items.Where(x => x.Tipo == 1 && x.Cantidad>0).OrderByDescending(x => x.Denominacion).ToList();
                var tarjeta = await _tarjetaService.GetTarjetaById(id);
                if(!validarDesgaste(items, cash))
                {
                    return Ok(new { success = false, message = "El monto no puede ser retirado" });
                }else if (tarjeta == null)
                {
                    return Ok(new { success = false, message = "Cliente no encontrado" });
                }
                else if (tarjeta.Saldo < cash)
                {
                    return Ok(new { success = false, message = "Saldo no disponible" });
                }else if (type == 1)
                {
                    var itemsPagado = new List<ATMDTO>();
                    foreach (var item in items)
                    {
                        var cantidad = (int)(cash / item.Denominacion);
                        cash %= item.Denominacion;
                        if (cantidad > 0)
                        {

                            item.Cantidad -= cantidad;
                            item.Fecha_Actualizado = DateTime.Now;
                            _ATMService.UpdateATM(item);
                            itemsPagado.Add(item);
                        }
                    }
                    tarjeta.Fecha_Actualizado = DateTime.Now;
                    if (tarjeta.Tipo == 1)
                    {
                        tarjeta.Saldo -= cash;
                    }
                    else
                    {
                        tarjeta.Saldo += cash;
                    }
                    _tarjetaService.UpdateTarjeta(tarjeta);


                }
                return Ok(new { success = true, message = "Disponible" });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        private bool validarDesgaste(List<ATMDTO> atm, int importe)
        {
            double residual = 0.00;
            foreach (var item in atm)
            {
                importe %= item.Denominacion;
            }
            return (importe == 0);
        }
    }
}
