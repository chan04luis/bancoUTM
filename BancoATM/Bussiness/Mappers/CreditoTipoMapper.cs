public class CreditoTipoMapper : ICreditoTipoMapper
{
    public CreditoTipoDTO MapToDTO(CreditoTipo creditoTipo)
    {
        return new CreditoTipoDTO
        {
            Id = creditoTipo.Id,
            Nombre = creditoTipo.Nombre,
            TasaAnual = creditoTipo.TasaAnual,
            EdadMinima = creditoTipo.EdadMinima,
            EdadMaxima = creditoTipo.EdadMaxima,
            CreditoMinimo = creditoTipo.CreditoMinimo,
            CreditoMaximo = creditoTipo.CreditoMaximo,
            Estatus = creditoTipo.Estatus,
            FechaRegistro = creditoTipo.FechaRegistro,
            FechaActualizado = creditoTipo.FechaActualizado
        };
    }

    public CreditoTipo MapToEntity(CreditoTipoDTO creditoTipoDTO)
    {
        return new CreditoTipo
        {
            Id = creditoTipoDTO.Id,
            Nombre = creditoTipoDTO.Nombre,
            TasaAnual = creditoTipoDTO.TasaAnual,
            EdadMinima = creditoTipoDTO.EdadMinima,
            EdadMaxima = creditoTipoDTO.EdadMaxima,
            CreditoMinimo = creditoTipoDTO.CreditoMinimo,
            CreditoMaximo = creditoTipoDTO.CreditoMaximo,
            Estatus = creditoTipoDTO.Estatus,
            FechaRegistro = creditoTipoDTO.FechaRegistro,
            FechaActualizado = creditoTipoDTO.FechaActualizado
        };
    }
}