using System;
using System.Collections.Generic;

public interface IServicioService
{
    Task<List<Servicio>> GetAllServicios();
    Task<Servicio> GetServicioById(int id);
    Task<Servicio> AddServicio(Servicio servicio);
    Task<Servicio> UpdateServicio(Servicio servicio);
    Task<int> DeleteServicio(int id);
}
