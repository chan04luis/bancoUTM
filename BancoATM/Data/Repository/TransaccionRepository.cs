using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;

public class TransaccionRepository : ITransaccionRepository
{
    private readonly ApplicationDbContext _context;

    public TransaccionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaccion>> GetAll()
    {
        return _context.Transaccions.AsNoTracking().Include(u => u.TipoTransaccion).ToList();
    }

    public async Task<Transaccion> GetById(int id)
    {
        return _context.Transaccions.AsNoTracking().Include(u => u.TipoTransaccion).FirstOrDefault(t => t.Id == id);
    }

    public async Task<Transaccion> Add(Transaccion transaccion)
    {
        _context.Transaccions.Add(transaccion);
        _context.SaveChanges();
        return transaccion;
    }

    public async Task<Transaccion> Update(Transaccion transaccion)
    {
        _context.Entry(transaccion).State = EntityState.Modified;
        _context.SaveChanges();
        return transaccion;
    }

    public async Task<int> Delete(Transaccion transaccion)
    {
        _context.Transaccions.Remove(transaccion);
        return _context.SaveChanges();
    }
}
