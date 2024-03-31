public interface ICreditoRepository
{
    Task<List<Credito>> GetAll();
    Task<Credito> GetById(int id);
    Task<Credito> Insert(Credito credito, List<CreditoPagos> creditoPagos);
    Task<Credito> Update(Credito credito);
    Task<int> Delete(int id);
}