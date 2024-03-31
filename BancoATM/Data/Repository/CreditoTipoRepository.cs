using Data;
using Microsoft.EntityFrameworkCore;

public class CreditoTipoRepository : ICreditoTipoRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CreditoTipoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CreditoTipo>> GetAll()
    {
        return await _dbContext.CreditosTipo.AsNoTracking().ToListAsync();
    }

    public async Task<CreditoTipo> GetById(int id)
    {
        return _dbContext.CreditosTipo.AsNoTracking().FirstOrDefault(x=>x.Id==id);
    }

    public async Task<CreditoTipo> Add(CreditoTipo creditoTipo)
    {
        _dbContext.CreditosTipo.Add(creditoTipo);
        await _dbContext.SaveChangesAsync();
        return creditoTipo;
    }

    public async Task<CreditoTipo> Update(CreditoTipo creditoTipo)
    {
        _dbContext.Entry(creditoTipo).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return creditoTipo;
    }

    public async Task<int> Delete(CreditoTipo creditoTipo)
    {
        _dbContext.CreditosTipo.Remove(creditoTipo);
        return await _dbContext.SaveChangesAsync();
    }
}