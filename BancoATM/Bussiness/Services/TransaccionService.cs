using System;
using System.Collections.Generic;

public class TransaccionService : ITransaccionService
{
    private readonly ITransaccionRepository _transaccionRepository;

    public TransaccionService(ITransaccionRepository transaccionRepository)
    {
        _transaccionRepository = transaccionRepository;
    }

    public async Task<List<TransaccionDTO>> GetAllTransacciones()
    {
        var items = await _transaccionRepository.GetAll();
        List<TransaccionDTO> list = new List<TransaccionDTO>();
        items.ForEach(item =>
        {
            list.Add(Mapper.MapTransactionsToDTO(item));
        });
        return list;
    }

    public async Task<TransaccionDTO> GetTransaccionById(int id)
    {
        return Mapper.MapTransactionsToDTO(await _transaccionRepository.GetById(id));
    }

    public async Task<TransaccionDTO> AddTransaccion(TransaccionDTO transaccion)
    {
        var item = await _transaccionRepository.Add(Mapper.MapTransactionsToDTO(transaccion));
        return Mapper.MapTransactionsToDTO(item);
    }

    public async Task<TransaccionDTO> UpdateTransaccion(TransaccionDTO transaccion)
    {
        var item = await _transaccionRepository.Update(Mapper.MapTransactionsToDTO(transaccion));
        return Mapper.MapTransactionsToDTO(item);
    }

    public async Task<int> DeleteTransaccion(int id)
    {
        var transaccion = await _transaccionRepository.GetById(id);
        if (transaccion != null)
        {
            return await _transaccionRepository.Delete(transaccion);
        }
        return 0;
    }
}
