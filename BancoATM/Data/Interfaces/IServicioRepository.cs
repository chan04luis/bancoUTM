using System;
using System.Collections.Generic;

public interface IServicioRepository
{
    List<Servicio> GetAll();
    Servicio GetById(int id);
    void Add(Servicio servicio);
    void Update(Servicio servicio);
    void Delete(Servicio servicio);
}
