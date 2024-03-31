public interface ICreditoPagosRepository
{
    Task<CreditoPagos> GetById(int id);
    Task<CreditoPagos> Update(CreditoPagos creditoPagos);
    Task<int> Delete(int id);
    Task<List<CreditoPagos>> GetByCreditoId(int idCredito);
}