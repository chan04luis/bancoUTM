using System;
using System.Collections.Generic;

public interface IServicioRepository
{
    Task<List<Servicio>> GetAll();
    Task<Servicio> GetById(int id);
    Task<Servicio> Add(Servicio servicio);
    Task<Servicio> Update(Servicio servicio);
    Task<int> Delete(Servicio servicio);
}
