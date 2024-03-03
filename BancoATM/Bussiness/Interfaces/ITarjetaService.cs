using System;
using System.Collections.Generic;

public interface ITarjetaService
{
    Tarjeta GetTarjetaById(int id);
    Tarjeta GetTarjetaByNip(string tarjeta);
    List<Tarjeta> GetAllTarjetas();
    void AddTarjeta(Tarjeta tarjeta);
    void UpdateTarjeta(Tarjeta tarjeta);
    void DeleteTarjeta(int id);
}
