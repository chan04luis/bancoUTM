using System;
using System.Collections.Generic;

public interface ITipoTransaccionRepository
{
    Task<TipoTransaccion?> GetById(int id);
    Task<List<TipoTransaccion>> GetAll();
    Task<TipoTransaccion?> Add(TipoTransaccion tipoTransaccion);
    Task<TipoTransaccion?> Update(TipoTransaccion tipoTransaccion);
    Task<int?> Delete(TipoTransaccion tipoTransaccion);
}