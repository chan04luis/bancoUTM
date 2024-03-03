using System;
using System.Collections.Generic;

public interface ITransaccionRepository
{
    List<Transaccion> GetAll();
    Transaccion GetById(int id);
    void Add(Transaccion transaccion);
    void Update(Transaccion transaccion);
    void Delete(Transaccion transaccion);
}
