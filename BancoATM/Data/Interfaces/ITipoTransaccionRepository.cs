using System;
using System.Collections.Generic;

public interface ITipoTransaccionRepository
{
    TipoTransaccion GetById(int id);
    List<TipoTransaccion> GetAll();
    void Add(TipoTransaccion tipoTransaccion);
    void Update(TipoTransaccion tipoTransaccion);
    void Delete(TipoTransaccion tipoTransaccion);
}