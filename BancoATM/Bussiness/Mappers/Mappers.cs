using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.NetworkInformation;

public static class Mapper
{
    public static ClienteDTO? MapClienteToDTO(Cliente? cliente)
    {
        if(cliente!=null)
        {
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };
        }else
        {
            return null;
        }
    }

    public static TarjetaDTO MapTarjetaToDTO(Tarjeta tarjeta)
    {
        return new TarjetaDTO
        {
            Id = tarjeta.Id,
            Tarjeta = tarjeta.tarjeta,
            Limite = tarjeta.Limite,
            Nip = tarjeta.NIP,
            Tipo = tarjeta.Tipo,
            Saldo = tarjeta.Saldo,
            Fecha_Actualizado = tarjeta.Fecha_Actualizado,
            Fecha_Registro = tarjeta.Fecha_Registro,
            Cliente = MapClienteToDTO(tarjeta.Cliente)
        };
    }
    public static Tarjeta MapTarjetaToDTO(TarjetaDTO tarjeta)
    {
        return new Tarjeta
        {
            Id = tarjeta.Id,
            tarjeta = tarjeta.Tarjeta,
            Limite = tarjeta.Limite,
            NIP = tarjeta.Nip,
            Tipo = tarjeta.Tipo,
            Saldo = tarjeta.Saldo,
            Fecha_Actualizado = tarjeta.Fecha_Actualizado,
            Fecha_Registro = tarjeta.Fecha_Registro,
            Id_Cliente = tarjeta.Cliente.Id
        };
    }
    public static ATMDTO MapATMToDTO(ATM atm)
    {
        return new ATMDTO
        {
            Id = atm.Id,
            Denominacion = atm.Denominacion,
            Cantidad = atm.Cantidad,
            Estatus = atm.Estatus,
            Tipo = atm.Tipo,
            Fecha_Registro = atm.Fecha_Registro,
            Fecha_Actualizado = atm.Fecha_Actualizado
        };
    }

    public static ATM MapATMToDTO(ATMDTO atm)
    {
        return new ATM
        {
            Id = atm.Id,
            Denominacion = atm.Denominacion,
            Cantidad = atm.Cantidad,
            Estatus = atm.Estatus,
            Tipo = atm.Tipo,
            Fecha_Registro = atm.Fecha_Registro,
            Fecha_Actualizado = atm.Fecha_Actualizado
        };
    }



    public static Transaccion MapTransactionsToDTO(TransaccionDTO transaccion)
    {
        return new Transaccion
        {
            Id=transaccion.Id,
            Edo_cuenta=transaccion.Edo_cuenta,
            Id_Tipo=transaccion.Id_Tipo,
            Estatus=transaccion.Estatus,
            Descripcion = transaccion.Descripcion,
            TipoTransaccion=transaccion.TipoTransaccion,
            Importe = transaccion.Importe,
            Referencia = transaccion.Referencia,
            Id_Cuenta = transaccion.Id_Cuenta,
            Fecha_Registro = transaccion.Fecha_Registro,
            Fecha_Actualizado = transaccion.Fecha_Actualizado
        };
    }
    public static TransaccionDTO MapTransactionsToDTO(Transaccion transaccion)
    {
        return new TransaccionDTO
        {
            Id = transaccion.Id,
            Edo_cuenta = transaccion.Edo_cuenta,
            Id_Tipo = transaccion.Id_Tipo,
            Estatus = transaccion.Estatus,
            Descripcion = transaccion.Descripcion,
            TipoTransaccion = transaccion.TipoTransaccion,
            Importe = transaccion.Importe,
            Referencia = transaccion.Referencia,
            Id_Cuenta = transaccion.Id_Cuenta,
            Fecha_Registro = transaccion.Fecha_Registro,
            Fecha_Actualizado = transaccion.Fecha_Actualizado
        };
    }
}
