using System;
using System.Collections.Generic;

public interface ITransaccionService
{
    Task<List<TransaccionDTO>> GetAllTransacciones();
    Task<TransaccionDTO> GetTransaccionById(int id);
    Task<TransaccionDTO> AddTransaccion(TransaccionDTO transaccion);
    Task<TransaccionDTO> UpdateTransaccion(TransaccionDTO transaccion);
    Task<int> DeleteTransaccion(int id);
}
