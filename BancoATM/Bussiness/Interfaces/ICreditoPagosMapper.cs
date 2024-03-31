public interface ICreditoPagosMapper
{
    CreditoPagosDTO MapToDTO(CreditoPagos creditoPagos);
    CreditoPagos MapToEntity(CreditoPagosDTO creditoPagosDTO);
}