
using Data;
using Microsoft.EntityFrameworkCore;

public class TarjetaRepository : ITarjetaRepository
{
    private readonly ApplicationDbContext _context;

    public TarjetaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Tarjeta GetById(int id)
    {
        return _context.Tarjetas.Where(t => t.Id == id).Include(u => u.Cliente).FirstOrDefault();
    }

    public Tarjeta GetByTD(string tarjeta)
    {
        return _context.Tarjetas.Where(t => t.tarjeta == tarjeta).Include(u => u.Cliente).FirstOrDefault();
    }

    public List<Tarjeta> GetAll()
    {
        return _context.Tarjetas.ToList();
    }

    public void Add(Tarjeta tarjeta)
    {
        _context.Tarjetas.Add(tarjeta);
        _context.SaveChanges();
    }

    public void Update(Tarjeta tarjeta)
    {
        _context.Entry(tarjeta).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Tarjeta tarjeta)
    {
        _context.Tarjetas.Remove(tarjeta);
        _context.SaveChanges();
    }
}
