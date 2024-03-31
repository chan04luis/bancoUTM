
using Data;
using Microsoft.EntityFrameworkCore;

public class TarjetaRepository : ITarjetaRepository
{
    private readonly ApplicationDbContext _context;

    public TarjetaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Tarjeta> GetById(int id)
    {
        return _context.Tarjetas.AsNoTracking().Where(t => t.Id == id).Include(u => u.Cliente).FirstOrDefault();
    }

    public async Task<Tarjeta> GetByTD(string tarjeta)
    {
        return _context.Tarjetas.AsNoTracking().Where(t => t.tarjeta == tarjeta).Include(u => u.Cliente).FirstOrDefault();
    }

    public async Task<List<Tarjeta>> GetAll()
    {
        return _context.Tarjetas.AsNoTracking().ToList();
    }

    public async Task<Tarjeta> Add(Tarjeta tarjeta)
    {
        _context.Tarjetas.Add(tarjeta);
        _context.SaveChanges();
        return tarjeta;
    }

    public async Task<Tarjeta> Update(Tarjeta tarjeta)
    {
        _context.Entry(tarjeta).State = EntityState.Modified;
        _context.SaveChanges();
        return tarjeta;
    }

    public async Task<int> Delete(Tarjeta tarjeta)
    {
        _context.Tarjetas.Remove(tarjeta);
        return _context.SaveChanges();
    }
}
