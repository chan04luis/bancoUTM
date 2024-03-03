using System;
using System.Collections.Generic;

public interface ITransaccionService
{
    List<Transaccion> GetAllTransacciones();
    Transaccion GetTransaccionById(int id);
    void AddTransaccion(Transaccion transaccion);
    void UpdateTransaccion(Transaccion transaccion);
    void DeleteTransaccion(int id);
}
