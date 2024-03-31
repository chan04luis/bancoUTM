using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.EntityFrameworkCore;

public class ATMRepository : IATMRepository
{
    private readonly ApplicationDbContext _context;

    public ATMRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ATM>> GetAll()
    {
        return _context.ATMs.AsNoTracking().ToList();
    }

    public async Task<ATM> GetById(int id)
    {
        return _context.ATMs.AsNoTracking().FirstOrDefault(a => a.Id == id);
    }

    public async Task<ATM> Add(ATM atm)
    {
        _context.ATMs.Add(atm);
        _context.SaveChanges();
        return atm;
    }

    public async Task<ATM> Update(ATM atm)
    {
        _context.Entry(atm).State = EntityState.Modified;
        _context.SaveChanges();
        return atm;
    }

    public async Task<int> Delete(ATM atm)
    {
        _context.ATMs.Remove(atm);
        return _context.SaveChanges();
    }
}
