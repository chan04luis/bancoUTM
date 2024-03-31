using System;
using System.Collections.Generic;

public class ServicioService : IServicioService
{
    private readonly IServicioRepository _servicioRepository;

    public ServicioService(IServicioRepository servicioRepository)
    {
        _servicioRepository = servicioRepository;
    }

    public async Task< List<Servicio>> GetAllServicios()
    {
        return await _servicioRepository.GetAll();
    }

    public async Task<Servicio> GetServicioById(int id)
    {
        return await _servicioRepository.GetById(id);
    }

    public async Task<Servicio> AddServicio(Servicio servicio)
    {
        return await _servicioRepository.Add(servicio);
    }

    public async Task<Servicio> UpdateServicio(Servicio servicio)
    {
        return await _servicioRepository.Update(servicio);
    }

    public async Task<int> DeleteServicio(int id)
    {
        var servicio = await _servicioRepository.GetById(id);
        if (servicio != null)
        {
            return await _servicioRepository.Delete(servicio);
        }
        return 0;
    }
}
