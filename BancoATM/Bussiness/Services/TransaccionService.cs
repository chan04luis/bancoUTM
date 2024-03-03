using System;
using System.Collections.Generic;

public class TransaccionService : ITransaccionService
{
    private readonly ITransaccionRepository _transaccionRepository;

    public TransaccionService(ITransaccionRepository transaccionRepository)
    {
        _transaccionRepository = transaccionRepository;
    }

    public List<Transaccion> GetAllTransacciones()
    {
        return _transaccionRepository.GetAll();
    }

    public Transaccion GetTransaccionById(int id)
    {
        return _transaccionRepository.GetById(id);
    }

    public void AddTransaccion(Transaccion transaccion)
    {
        _transaccionRepository.Add(transaccion);
    }

    public void UpdateTransaccion(Transaccion transaccion)
    {
        _transaccionRepository.Update(transaccion);
    }

    public void DeleteTransaccion(int id)
    {
        var transaccion = _transaccionRepository.GetById(id);
        if (transaccion != null)
        {
            _transaccionRepository.Delete(transaccion);
        }
    }
}
