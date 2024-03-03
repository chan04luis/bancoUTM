using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;

public class TipoTransaccionRepository : ITipoTransaccionRepository
{
    private readonly ApplicationDbContext _context;

    public TipoTransaccionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public TipoTransaccion GetById(int id)
    {
        return _context.TipoTransaccions.Where(t => t.Id == id).FirstOrDefault();
    }

    public List<TipoTransaccion> GetAll()
    {
        return _context.TipoTransaccions.ToList();
    }

    public void Add(TipoTransaccion tipoTransaccion)
    {
        _context.TipoTransaccions.Add(tipoTransaccion);
        _context.SaveChanges();
    }

    public void Update(TipoTransaccion tipoTransaccion)
    {
        _context.Entry(tipoTransaccion).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(TipoTransaccion tipoTransaccion)
    {
        _context.TipoTransaccions.Remove(tipoTransaccion);
        _context.SaveChanges();
    }
}
