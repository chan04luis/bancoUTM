public interface ICreditoPagosService
{
    Task<CreditoPagosDTO> GetCreditoPagosById(int id);
    Task<CreditoPagosDTO> UpdateCreditoPagos(CreditoPagosDTO creditoPagosDTO);
    Task<int> DeleteCreditoPagos(int id);
    Task<List<CreditoPagosDTO>> GetCreditoPagosByCreditoId(int idCredito);
}
