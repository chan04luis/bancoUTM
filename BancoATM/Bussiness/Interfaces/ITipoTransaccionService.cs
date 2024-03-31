using System;
using System.Collections.Generic;

public interface ITipoTransaccionService
{
    Task<TipoTransaccion> GetTipoTransaccionById(int id);
    Task<List<TipoTransaccion>> GetAllTipoTransacciones();
    Task<TipoTransaccion> AddTipoTransaccion(TipoTransaccion tipoTransaccion);
    Task<TipoTransaccion> UpdateTipoTransaccion(TipoTransaccion tipoTransaccion);
    Task<int?> DeleteTipoTransaccion(int id);
}
