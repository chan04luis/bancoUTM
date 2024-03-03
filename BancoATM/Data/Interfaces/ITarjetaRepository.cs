using System;
using System.Collections.Generic;

public interface ITarjetaRepository
{
    Tarjeta GetById(int id);
    List<Tarjeta> GetAll();
    void Add(Tarjeta tarjeta);
    void Update(Tarjeta tarjeta);
    void Delete(Tarjeta tarjeta);
    Tarjeta GetByTD(string tarjeta);
}
