using System;
using System.Collections.Generic;

public class TipoTransaccionService : ITipoTransaccionService
{
    private readonly ITipoTransaccionRepository _tipoTransaccionRepository;

    public TipoTransaccionService(ITipoTransaccionRepository tipoTransaccionRepository)
    {
        _tipoTransaccionRepository = tipoTransaccionRepository;
    }

    public async Task<TipoTransaccion> GetTipoTransaccionById(int id)
    {
        return await _tipoTransaccionRepository.GetById(id);
    }

    public async Task< List<TipoTransaccion>> GetAllTipoTransacciones()
    {
        return await _tipoTransaccionRepository.GetAll();
    }

    public async Task<TipoTransaccion> AddTipoTransaccion(TipoTransaccion tipoTransaccion)
    {
        return await _tipoTransaccionRepository.Add(tipoTransaccion);
    }

    public async Task<TipoTransaccion> UpdateTipoTransaccion(TipoTransaccion tipoTransaccion)
    {
        return await _tipoTransaccionRepository.Update(tipoTransaccion);
    }

    public async Task<int?> DeleteTipoTransaccion(int id)
    {
        var tipoTransaccion = await _tipoTransaccionRepository.GetById(id);
        if (tipoTransaccion != null)
        {
            return await _tipoTransaccionRepository.Delete(tipoTransaccion);
        }
        return 0;
    }
}
