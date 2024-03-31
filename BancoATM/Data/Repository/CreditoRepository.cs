using Data;
using Microsoft.EntityFrameworkCore;

public class CreditoRepository : ICreditoRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CreditoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Credito>> GetAll()
    {
        return await _dbContext.Creditos.AsNoTracking()
            .Include(x=>x.CreditoTipo)
            .Include(x=>x.Cliente).ToListAsync();
    }

    public async Task<Credito> GetById(int id)
    {
        return _dbContext.Creditos.AsNoTracking()
            .Include(x => x.CreditoTipo)
            .Include(x => x.Cliente).FirstOrDefault(x=> x.Id==  id);
    }

    public async Task<Credito> Insert(Credito credito, List<CreditoPagos> creditoPagos)
    {
        _dbContext.Creditos.Add(credito);
        await _dbContext.SaveChangesAsync();

        foreach (var pago in creditoPagos)
        {
            pago.IdCredito = credito.Id;
            _dbContext.CreditosPagos.Add(pago);
        }
        await _dbContext.SaveChangesAsync();

        return credito;
    }

    public async Task<Credito> Update(Credito credito)
    {
        _dbContext.Entry(credito).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return credito;
    }

    public async Task<int> Delete(int id)
    {
        var credito = await _dbContext.Creditos.FindAsync(id);
        if (credito == null)
        {
            return 0;
        }

        _dbContext.Creditos.Remove(credito);
        return await _dbContext.SaveChangesAsync();
    }
}