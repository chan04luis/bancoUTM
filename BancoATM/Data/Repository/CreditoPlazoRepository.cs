
using Data;
using Microsoft.EntityFrameworkCore;

public class CreditoPlazoRepository : ICreditoPlazoRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CreditoPlazoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CreditoPlazo>> GetAll()
    {
        return await _dbContext.CreditosPlazos.AsNoTracking().Include(x => x.CreditoTipo).ToListAsync();
    }
    public async Task<List<CreditoPlazo>> GetAllByTipo(int idTipo)
    {
        return await _dbContext.CreditosPlazos.Where(x=>x.IdTipoCredito== idTipo).AsNoTracking().Include(x => x.CreditoTipo).ToListAsync();
    }

    public async Task<CreditoPlazo> GetById(int id)
    {
        return _dbContext.CreditosPlazos.AsNoTracking().Include(x=>x.CreditoTipo).FirstOrDefault(x=>x.Id== id);
    }

    public async Task<CreditoPlazo> Add(CreditoPlazo creditoPlazo)
    {
        _dbContext.CreditosPlazos.Add(creditoPlazo);
        await _dbContext.SaveChangesAsync();
        return creditoPlazo;
    }

    public async Task<CreditoPlazo> Update(CreditoPlazo creditoPlazo)
    {
        _dbContext.Entry(creditoPlazo).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return creditoPlazo;
    }

    public async Task<int> Delete(int id)
    {
        var creditoPlazo = await _dbContext.CreditosPlazos.FindAsync(id);
        if (creditoPlazo == null)
        {
            return 0;
        }

        _dbContext.CreditosPlazos.Remove(creditoPlazo);
        return await _dbContext.SaveChangesAsync();
    }
}