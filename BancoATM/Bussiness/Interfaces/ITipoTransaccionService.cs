using System;
using System.Collections.Generic;

public interface ITipoTransaccionService
{
    TipoTransaccion GetTipoTransaccionById(int id);
    List<TipoTransaccion> GetAllTipoTransacciones();
    void AddTipoTransaccion(TipoTransaccion tipoTransaccion);
    void UpdateTipoTransaccion(TipoTransaccion tipoTransaccion);
    void DeleteTipoTransaccion(int id);
}
