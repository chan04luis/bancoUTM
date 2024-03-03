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

    public List<ATM> GetAll()
    {
        return _context.ATMs.ToList();
    }

    public ATM GetById(int id)
    {
        return _context.ATMs.FirstOrDefault(a => a.Id == id);
    }

    public void Add(ATM atm)
    {
        _context.ATMs.Add(atm);
        _context.SaveChanges();
    }

    public void Update(ATM atm)
    {
        _context.Entry(atm).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(ATM atm)
    {
        _context.ATMs.Remove(atm);
        _context.SaveChanges();
    }
}
