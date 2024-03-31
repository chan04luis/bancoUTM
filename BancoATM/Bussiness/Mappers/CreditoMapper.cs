public class CreditoMapper : ICreditoMapper
{
    private readonly ICreditoTipoMapper creditoTipoMapper;
    public CreditoMapper(ICreditoTipoMapper creditoTipoMapper)
    {
        this.creditoTipoMapper = creditoTipoMapper;
    }
    public CreditoDTO MapToDTO(Credito credito)
    {
        return new CreditoDTO
        {
            Id = credito.Id,
            Estatus = credito.Estatus,
            IdTipoCredito = credito.IdTipoCredito,
            PlazoMeses = credito.PlazoMeses,
            TasaInteres = credito.TasaInteres,
            CreditoOtorgado = credito.CreditoOtorgado,
            FechaInicio = credito.FechaInicio,
            FechaFinal = credito.FechaFinal,
            IdCliente = credito.IdCliente,
            Pago = credito.Pago,
            FechaRegistro = credito.FechaRegistro,
            FechaActualizado = credito.FechaActualizado,
            clienteDTO= Mapper.MapClienteToDTO(credito.Cliente),
            creditoTipoDTO=creditoTipoMapper.MapToDTO(credito.CreditoTipo)
        };
    }

    public Credito MapToEntity(CreditoDTO creditoDTO)
    {
        return new Credito
        {
            Id = creditoDTO.Id,
            Estatus = creditoDTO.Estatus,
            IdTipoCredito = creditoDTO.IdTipoCredito,
            PlazoMeses = creditoDTO.PlazoMeses,
            TasaInteres = creditoDTO.TasaInteres,
            CreditoOtorgado = creditoDTO.CreditoOtorgado,
            FechaInicio = creditoDTO.FechaInicio,
            FechaFinal = creditoDTO.FechaFinal,
            IdCliente = creditoDTO.IdCliente,
            Pago = creditoDTO.Pago,
            FechaRegistro = creditoDTO.FechaRegistro,
            FechaActualizado = creditoDTO.FechaActualizado
        };
    }
}