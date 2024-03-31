public interface ICreditoService
{
    Task<List<CreditoDTO>> GetAllCreditos();
    Task<CreditoDTO> GetCreditoById(int id);
    Task<CreditoDTO> CreateCredito(CreditoDTO creditoDTO, List<CreditoPagosDTO> creditoPagosDTO);
    Task<CreditoDTO> UpdateCredito(CreditoDTO creditoDTO);
    Task<int> DeleteCredito(int id);
}