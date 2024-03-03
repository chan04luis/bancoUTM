using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Cliente GetById(int id)
    {
        return _context.Clientes.FirstOrDefault(c => c.Id == id);
    }

    public List<Cliente> GetAll()
    {
        return _context.Clientes.ToList();
    }

    public void Add(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
    }

    public void Update(Cliente cliente)
    {
        _context.Entry(cliente).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
    }
}
