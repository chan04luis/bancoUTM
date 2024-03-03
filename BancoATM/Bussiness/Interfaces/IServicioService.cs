using System;
using System.Collections.Generic;

public interface IServicioService
{
    List<Servicio> GetAllServicios();
    Servicio GetServicioById(int id);
    void AddServicio(Servicio servicio);
    void UpdateServicio(Servicio servicio);
    void DeleteServicio(int id);
}
