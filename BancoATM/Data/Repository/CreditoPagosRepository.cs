using Data;
using Microsoft.EntityFrameworkCore;

public class CreditoPagosRepository : ICreditoPagosRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CreditoPagosRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreditoPagos> GetById(int id)
    {
        return _dbContext.CreditosPagos.AsNoTracking().FirstOrDefault(x=>x.Id== id);
    }

    public async Task<CreditoPagos> Update(CreditoPagos creditoPagos)
    {
        _dbContext.Entry(creditoPagos).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return creditoPagos;
    }

    public async Task<int> Delete(int id)
    {
        var creditoPagos = await _dbContext.CreditosPagos.FindAsync(id);
        if (creditoPagos == null)
        {
            return 0;
        }

        _dbContext.CreditosPagos.Remove(creditoPagos);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CreditoPagos>> GetByCreditoId(int idCredito)
    {
        return await _dbContext.CreditosPagos.AsNoTracking().Where(cp => cp.IdCredito == idCredito).ToListAsync();
    }
}