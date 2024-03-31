public interface ICreditoMapper
{
    CreditoDTO MapToDTO(Credito credito);
    Credito MapToEntity(CreditoDTO creditoDTO);
}