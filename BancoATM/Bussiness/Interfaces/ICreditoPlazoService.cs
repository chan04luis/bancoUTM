public interface ICreditoPlazoService
{
    Task<List<CreditoPlazoDTO>> GetAllCreditosPlazo();
    Task<List<CreditoPlazoDTO>> GetAllCreditosPlazoByTipo(int idTipo);
    Task<CreditoPlazoDTO> GetCreditoPlazoById(int id);
    Task<CreditoPlazoDTO> AddCreditoPlazo(CreditoPlazoDTO creditoPlazoDTO);
    Task<CreditoPlazoDTO> UpdateCreditoPlazo(CreditoPlazoDTO creditoPlazoDTO);
    Task<int> DeleteCreditoPlazo(int id);
}