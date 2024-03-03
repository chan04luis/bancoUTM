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

    public List<Transaccion> GetAll()
    {
        return _context.Transaccions.Include(u => u.TipoTransaccion).ToList();
    }

    public Transaccion GetById(int id)
    {
        return _context.Transaccions.Include(u => u.TipoTransaccion).FirstOrDefault(t => t.Id == id);
    }

    public void Add(Transaccion transaccion)
    {
        _context.Transaccions.Add(transaccion);
        _context.SaveChanges();
    }

    public void Update(Transaccion transaccion)
    {
        _context.Entry(transaccion).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Transaccion transaccion)
    {
        _context.Transaccions.Remove(transaccion);
        _context.SaveChanges();
    }
}
