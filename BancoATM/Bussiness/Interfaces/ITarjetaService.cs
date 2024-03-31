using System;
using System.Collections.Generic;

public interface ITarjetaService
{
    Task<TarjetaDTO> GetTarjetaById(int id);
    Task<TarjetaDTO> GetTarjetaByNip(string tarjeta);
    Task<List<TarjetaDTO>> GetAllTarjetas();
    Task<TarjetaDTO> AddTarjeta(Tarjeta tarjeta);
    Task<TarjetaDTO> UpdateTarjeta(TarjetaDTO tarjeta);
    Task<int> DeleteTarjeta(int id);
    Task<(bool, string)> validateNip(string nip, int id, bool validador);
}
