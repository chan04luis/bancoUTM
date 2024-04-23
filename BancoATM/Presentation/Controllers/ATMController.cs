using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        private readonly IATMService _ATMService;
        private readonly ITarjetaService _tarjetaService;
        private readonly ITransaccionService _transaccionService;
        private readonly ICorreoElectronicoService _correoElectronicoService;
        public ATMController(IATMService aTMService, ITarjetaService tarjetaService, ITransaccionService transaccionService, ICorreoElectronicoService correoElectronicoService)
        {
            _ATMService = aTMService;
            _tarjetaService = tarjetaService;
            _transaccionService = transaccionService;
            _correoElectronicoService = correoElectronicoService;
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
        [HttpGet("available/{id}/{cash}/{type}")]
        public async Task<IActionResult> descargarSaldo(int id, int cash, int type=0)
        {
            try
            {
                var retiro = cash;
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
                    var itemsEntregado = new List<ATMCashOut>();
                    foreach (var i in items)
                    {
                        var cantidad = (int)(cash / i.Denominacion);
                        cash %= i.Denominacion;
                        if (cantidad > 0)
                        {

                            i.Cantidad -= cantidad;
                            i.Fecha_Actualizado = DateTime.Now;
                            var result =  await _ATMService.UpdateATM(i);
                            itemsPagado.Add(i);
                            i.Cantidad = cantidad;
                            itemsEntregado.Add(new ATMCashOut() { Cantidad=cantidad, Denominacion=$"{i.Denominacion:F2}"});
                        }
                    }
                    tarjeta.Fecha_Actualizado = DateTime.Now;
                    if (tarjeta.Tipo == 1)
                    {
                        tarjeta.Saldo -= retiro;
                    }
                    else
                    {
                        tarjeta.Saldo += retiro;
                    }
                    TransaccionDTO transaccion = new TransaccionDTO();
                    transaccion.Descripcion = $"Retiro de ${retiro:F2} a tarjeta {tarjeta.Tarjeta} saldo actual ${tarjeta.Saldo}";
                    transaccion.Id_Cuenta = tarjeta.Id;
                    transaccion.Id_Tipo = 2;
                    transaccion.Edo_cuenta = tarjeta.Id;
                    transaccion.Referencia = "";
                    transaccion.Importe = retiro;
                    var item = await _transaccionService.AddTransaccion(transaccion);
                    _correoElectronicoService.EnviarCorreo(tarjeta.Cliente.Email, "Retiro de efectivo", transaccion.Descripcion);
                    var service = await _tarjetaService.UpdateTarjeta(tarjeta);
                    return Ok(new { success = true, message = "Disponible", cashList = itemsEntregado, transaccion = item, tarjeta = service });

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

        [HttpPost("deposit")]
        public async Task<IActionResult> readDeposit([FromBody] DepositCheckDTO depositCheckDTO)
        {

            var items = await _ATMService.GetAllATMs();
            items = items.Where(x => x.Tipo == 1 && x.Cantidad > 0).OrderByDescending(x => x.Denominacion).ToList();
            if(validarDesgaste(items,depositCheckDTO.monto))
            {
                var list = new List<DepositDTO>();
                foreach(var i in items)
                {
                    list.Add(new DepositDTO() { id = i.Id, cantidad = 0, denominacion = $"{i.Denominacion:F2}", valor=i.Denominacion });
                }
                return Ok(new { success = true, depositList=list, message = $"{depositCheckDTO.monto:F2}" });
            }
            else
            {
                return Ok(new {success=false, message="Monto de deposito no valido"});
            }
        }

        [HttpPut("deposit")]
        public async Task<IActionResult> writeDeposit([FromBody] DepositCheckDTO depositCheckDTO)
        {
            if(depositCheckDTO.depositDTOs?.Sum(x=>x.cantidad) == 0)
            {
                return Ok(new { success = false, message = "Ingrese un monto para continuar" });
            }
            else
            {
                var billetes = depositCheckDTO.depositDTOs?.Where(x => x.cantidad > 0);
                if(billetes.Sum(x=>x.cantidad*x.valor) >= depositCheckDTO.monto)
                {
                    var tarjeta = await _tarjetaService.GetTarjetaById(depositCheckDTO.id);
                    if (tarjeta == null)
                    {
                        return Ok(new { success = false, message = "No existe cuenta" });
                    }
                    else
                    {
                        decimal retiro = Convert.ToDecimal(depositCheckDTO.depositDTOs?.Where(x => x?.cantidad > 0).Sum(x => x?.valor * x?.cantidad));
                        if (tarjeta.Tipo == 1)
                        {
                            tarjeta.Saldo += retiro;
                        }
                        else
                        {
                            tarjeta.Saldo -= retiro;
                        }
                        var itemsEntregado = new List<ATMCashOut>();
                        if (retiro > depositCheckDTO.monto)
                        {
                            var cambio = retiro - depositCheckDTO.monto;
                            var dep = await _ATMService.GetAllATMs();
                            dep = dep.Where(x => x.Cantidad > 0).OrderByDescending(x => x.Denominacion).ToList();
                            foreach (var i in dep)
                            {
                                var cantidad = (int)(cambio / i.Denominacion);
                                cambio %= i.Denominacion;
                                if (cantidad > 0)
                                {
                                    itemsEntregado.Add(new ATMCashOut() { Cantidad = cantidad, Denominacion = $"{i.Denominacion:F2}", id = i.Id });
                                }
                            }
                        }
                        foreach (var i in depositCheckDTO.depositDTOs)
                        {
                            var atm = await _ATMService.GetATMById(i.id);
                            atm.Cantidad += i.cantidad;
                            var result = await _ATMService.UpdateATM(atm);
                        }
                        foreach (var i in itemsEntregado)
                        {
                            var atm = await _ATMService.GetATMById(i.id);
                            atm.Cantidad -= i.Cantidad;
                            var result = await _ATMService.UpdateATM(atm);
                        }
                        TransaccionDTO transaccion = new TransaccionDTO();
                        transaccion.Descripcion = $"Deposito de ${retiro:F2} a tarjeta {tarjeta.Tarjeta} saldo actual ${tarjeta.Saldo}";
                        transaccion.Id_Cuenta = tarjeta.Id;
                        transaccion.Id_Tipo = 1;
                        transaccion.Edo_cuenta = tarjeta.Id;
                        transaccion.Referencia = "";
                        transaccion.Importe = retiro;
                        var item = await _transaccionService.AddTransaccion(transaccion);
                        var service = await _tarjetaService.UpdateTarjeta(tarjeta);
                        _correoElectronicoService.EnviarCorreo(tarjeta.Cliente.Email, "Deposito de efectivo", transaccion.Descripcion);
                        return Ok(new { success = true, message = "Disponible", cashList = itemsEntregado, transaccion = item, tarjeta = service });
                    }
                }
                else
                {
                    return Ok(new { success = false, message = "Los billetes ingresados son menor al monto a depositar" });
                }
                
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
