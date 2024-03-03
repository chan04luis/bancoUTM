using System;
using System.Collections.Generic;

public class TipoTransaccionService : ITipoTransaccionService
{
    private readonly ITipoTransaccionRepository _tipoTransaccionRepository;

    public TipoTransaccionService(ITipoTransaccionRepository tipoTransaccionRepository)
    {
        _tipoTransaccionRepository = tipoTransaccionRepository;
    }

    public TipoTransaccion GetTipoTransaccionById(int id)
    {
        return _tipoTransaccionRepository.GetById(id);
    }

    public List<TipoTransaccion> GetAllTipoTransacciones()
    {
        return _tipoTransaccionRepository.GetAll();
    }

    public void AddTipoTransaccion(TipoTransaccion tipoTransaccion)
    {
        _tipoTransaccionRepository.Add(tipoTransaccion);
    }

    public void UpdateTipoTransaccion(TipoTransaccion tipoTransaccion)
    {
        _tipoTransaccionRepository.Update(tipoTransaccion);
    }

    public void DeleteTipoTransaccion(int id)
    {
        var tipoTransaccion = _tipoTransaccionRepository.GetById(id);
        if (tipoTransaccion != null)
        {
            _tipoTransaccionRepository.Delete(tipoTransaccion);
        }
    }
}
