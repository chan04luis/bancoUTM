public interface ICreditoTipoMapper
{
    CreditoTipoDTO MapToDTO(CreditoTipo creditoTipo);
    CreditoTipo MapToEntity(CreditoTipoDTO creditoTipoDTO);
}