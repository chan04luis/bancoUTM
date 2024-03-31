public class CreditoPlazoMapper : ICreditoPlazoMapper
{
    public CreditoPlazoDTO MapToDTO(CreditoPlazo creditoPlazo)
    {
        return new CreditoPlazoDTO
        {
            Id = creditoPlazo.Id,
            PlazoMeses = creditoPlazo.PlazoMeses,
            Estatus = creditoPlazo.Estatus,
            IdTipoCredito = creditoPlazo.IdTipoCredito,
            FechaRegistro = creditoPlazo.FechaRegistro,
            FechaActualizado = creditoPlazo.FechaActualizado
        };
    }

    public CreditoPlazo MapToEntity(CreditoPlazoDTO creditoPlazoDTO)
    {
        return new CreditoPlazo
        {
            Id = creditoPlazoDTO.Id,
            PlazoMeses = creditoPlazoDTO.PlazoMeses,
            Estatus = creditoPlazoDTO.Estatus,
            IdTipoCredito = creditoPlazoDTO.IdTipoCredito,
            FechaRegistro = creditoPlazoDTO.FechaRegistro,
            FechaActualizado = creditoPlazoDTO.FechaActualizado
        };
    }
}