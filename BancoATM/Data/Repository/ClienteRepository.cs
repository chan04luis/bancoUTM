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

    public async Task<Cliente> GetById(int id)
    {
        return _context.Clientes.AsNoTracking().FirstOrDefault(c => c.Id == id);
    }

    public async Task<List<Cliente>> GetAll()
    {
        return _context.Clientes.AsNoTracking().ToList();
    }

    public async Task<Cliente> Add(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
        return cliente;
    }

    public async Task<Cliente> Update(Cliente cliente)
    {
        _context.Entry(cliente).State = EntityState.Modified;
        _context.SaveChanges();
        return cliente;
    }

    public async Task<int> Delete(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        return _context.SaveChanges();
    }
}
