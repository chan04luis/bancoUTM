using System;
using System.Collections.Generic;

public interface ITransaccionRepository
{
    Task<List<Transaccion>> GetAll();
    Task<Transaccion> GetById(int id);
    Task<Transaccion> Add(Transaccion transaccion);
    Task<Transaccion> Update(Transaccion transaccion);
    Task<int> Delete(Transaccion transaccion);
}
