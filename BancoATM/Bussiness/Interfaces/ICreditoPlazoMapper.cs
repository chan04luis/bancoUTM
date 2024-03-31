public interface ICreditoPlazoMapper
{
    CreditoPlazoDTO MapToDTO(CreditoPlazo creditoPlazo);
    CreditoPlazo MapToEntity(CreditoPlazoDTO creditoPlazoDTO);
}