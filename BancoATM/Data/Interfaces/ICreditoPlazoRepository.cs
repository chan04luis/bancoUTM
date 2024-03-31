public interface ICreditoPlazoRepository
{
    Task<List<CreditoPlazo>> GetAll();
    Task<List<CreditoPlazo>> GetAllByTipo(int idTipo);
    Task<CreditoPlazo> GetById(int id);
    Task<CreditoPlazo> Add(CreditoPlazo creditoPlazo);
    Task<CreditoPlazo> Update(CreditoPlazo creditoPlazo);
    Task<int> Delete(int id);
}