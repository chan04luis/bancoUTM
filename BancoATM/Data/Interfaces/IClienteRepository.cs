
public interface IClienteRepository
{
    Task<Cliente> GetById(int id);
    Task<List<Cliente>> GetAll();
    Task<Cliente> Add(Cliente cliente);
    Task<Cliente> Update(Cliente cliente);
    Task<int> Delete(Cliente cliente);
}
