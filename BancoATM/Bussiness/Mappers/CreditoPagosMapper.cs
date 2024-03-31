public class CreditoPagosMapper : ICreditoPagosMapper
{
    public CreditoPagosDTO MapToDTO(CreditoPagos creditoPagos)
    {
        return new CreditoPagosDTO
        {
            Id = creditoPagos.Id,
            NumPago = creditoPagos.NumPago,
            Pago = creditoPagos.Pago,
            Fecha = creditoPagos.Fecha,
            FechaPago = creditoPagos.FechaPago,
            Capital = creditoPagos.Capital,
            Interes = creditoPagos.Interes,
            Estatus = creditoPagos.Estatus,
            IdCredito = creditoPagos.IdCredito,
            FechaRegistro = creditoPagos.FechaRegistro,
            FechaActualizado = creditoPagos.FechaActualizado
        };
    }

    public CreditoPagos MapToEntity(CreditoPagosDTO creditoPagosDTO)
    {
        return new CreditoPagos
        {
            Id = creditoPagosDTO.Id,
            NumPago = creditoPagosDTO.NumPago,
            Pago = creditoPagosDTO.Pago,
            Fecha = creditoPagosDTO.Fecha,
            FechaPago = creditoPagosDTO.FechaPago,
            Capital = creditoPagosDTO.Capital,
            Interes = creditoPagosDTO.Interes,
            Estatus = creditoPagosDTO.Estatus,
            IdCredito = creditoPagosDTO.IdCredito,
            FechaRegistro = creditoPagosDTO.FechaRegistro,
            FechaActualizado = creditoPagosDTO.FechaActualizado
        };
    }
}