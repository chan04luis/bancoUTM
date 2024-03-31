using System;
using System.Collections.Generic;

public interface ITarjetaRepository
{
    Task<Tarjeta> GetById(int id);
    Task<List<Tarjeta>> GetAll();
    Task<Tarjeta> Add(Tarjeta tarjeta);
    Task<Tarjeta> Update(Tarjeta tarjeta);
    Task<int> Delete(Tarjeta tarjeta);
    Task<Tarjeta> GetByTD(string tarjeta);
}
