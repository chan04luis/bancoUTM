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

    public async Task<List<Servicio>> GetAll()
    {
        return _context.Servicios.AsNoTracking().Include(x => x.Tarjeta).Include(x=>x.Tarjeta.Cliente).ToList();
    }

    public async Task<Servicio> GetById(int id)
    {
        return _context.Servicios.AsNoTracking().Include(x=>x.Tarjeta).Include(x => x.Tarjeta.Cliente).FirstOrDefault(s => s.Id == id);
    }

    public async Task<Servicio> Add(Servicio servicio)
    {
        _context.Servicios.Add(servicio);
        _context.SaveChanges();
        return servicio;
    }

    public async Task<Servicio> Update(Servicio servicio)
    {
        _context.Entry(servicio).State = EntityState.Detached;
        _context.SaveChanges();
        return servicio;
    }

    public async Task<int> Delete(Servicio servicio)
    {
        _context.Servicios.Remove(servicio);
        return _context.SaveChanges();
    }
}
