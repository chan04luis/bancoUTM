public interface ICreditoTipoRepository
{
    Task<List<CreditoTipo>> GetAll();
    Task<CreditoTipo> GetById(int id);
    Task<CreditoTipo> Add(CreditoTipo creditoTipo);
    Task<CreditoTipo> Update(CreditoTipo creditoTipo);
    Task<int> Delete(CreditoTipo creditoTipo);
}