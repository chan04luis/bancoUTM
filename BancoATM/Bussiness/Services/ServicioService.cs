using System;
using System.Collections.Generic;

public class ServicioService : IServicioService
{
    private readonly IServicioRepository _servicioRepository;

    public ServicioService(IServicioRepository servicioRepository)
    {
        _servicioRepository = servicioRepository;
    }

    public List<Servicio> GetAllServicios()
    {
        return _servicioRepository.GetAll();
    }

    public Servicio GetServicioById(int id)
    {
        return _servicioRepository.GetById(id);
    }

    public void AddServicio(Servicio servicio)
    {
        _servicioRepository.Add(servicio);
    }

    public void UpdateServicio(Servicio servicio)
    {
        _servicioRepository.Update(servicio);
    }

    public void DeleteServicio(int id)
    {
        var servicio = _servicioRepository.GetById(id);
        if (servicio != null)
        {
            _servicioRepository.Delete(servicio);
        }
    }
}
