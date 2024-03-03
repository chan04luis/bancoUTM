using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;

public class ServicioRepository : IServicioRepository
{
    private readonly ApplicationDbContext _context;

    public ServicioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Servicio> GetAll()
    {
        return _context.Servicios.Include(x => x.Tarjeta).Include(x=>x.Tarjeta.Cliente).ToList();
    }

    public Servicio GetById(int id)
    {
        return _context.Servicios.Include(x=>x.Tarjeta).Include(x => x.Tarjeta.Cliente).FirstOrDefault(s => s.Id == id);
    }

    public void Add(Servicio servicio)
    {
        _context.Servicios.Add(servicio);
        _context.SaveChanges();
    }

    public void Update(Servicio servicio)
    {
        _context.Entry(servicio).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Servicio servicio)
    {
        _context.Servicios.Remove(servicio);
        _context.SaveChanges();
    }
}
