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

    public async Task<TipoTransaccion?> GetById(int id)
    {
        return _context.TipoTransaccions.AsNoTracking().Where(t => t.Id == id).FirstOrDefault();
    }

    public async Task<List<TipoTransaccion>> GetAll()
    {
        return _context.TipoTransaccions.AsNoTracking().ToList();
    }

    public async Task<TipoTransaccion?> Add(TipoTransaccion tipoTransaccion)
    {
        _context.TipoTransaccions.Add(tipoTransaccion);
        _context.SaveChanges();
        return tipoTransaccion;
    }

    public async Task<TipoTransaccion?> Update(TipoTransaccion tipoTransaccion)
    {
        _context.Entry(tipoTransaccion).State = EntityState.Modified;
        _context.SaveChanges();
        return tipoTransaccion;
    }

    public async Task<int?> Delete(TipoTransaccion tipoTransaccion)
    {
        _context.TipoTransaccions.Remove(tipoTransaccion);
        return _context.SaveChanges();
    }
}
