using System;
using System.Collections.Generic;

public class TarjetaService : ITarjetaService
{
    private readonly ITarjetaRepository _tarjetaRepository;

    public TarjetaService(ITarjetaRepository tarjetaRepository)
    {
        _tarjetaRepository = tarjetaRepository;
    }

    public Tarjeta GetTarjetaById(int id)
    {
        return _tarjetaRepository.GetById(id);
    }


    public Tarjeta GetTarjetaByNip(string tarjeta)
    {
        return _tarjetaRepository.GetByTD(tarjeta);
    }
    public List<Tarjeta> GetAllTarjetas()
    {
        return _tarjetaRepository.GetAll();
    }

    public void AddTarjeta(Tarjeta tarjeta)
    {
        _tarjetaRepository.Add(tarjeta);
    }

    public void UpdateTarjeta(Tarjeta tarjeta)
    {
        _tarjetaRepository.Update(tarjeta);
    }

    public void DeleteTarjeta(int id)
    {
        var tarjeta = _tarjetaRepository.GetById(id);
        if (tarjeta != null)
        {
            _tarjetaRepository.Delete(tarjeta);
        }
    }
}
