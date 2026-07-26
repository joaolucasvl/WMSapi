using LogisticaAPI.Data;
using LogisticaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.Repositories;

public class TipoPaleteRepository : ITipoPaleteRepository
{
    private AppDbContext _dbContext;
    
    public TipoPaleteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<IEnumerable<TipoPalete>> GetAll()
    {
        var tipoPaletes = await _dbContext.TipoPaletes.AsNoTracking().ToListAsync();
        return tipoPaletes;
    }

    public async Task<TipoPalete?> GetById(Guid id)
    {
        return await _dbContext.TipoPaletes.FirstOrDefaultAsync(tp => tp.TipoPaleteId == id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var existsTipoPalete =  await GetById(id);
        
        if (existsTipoPalete == null)
            return false;
        
        _dbContext.TipoPaletes.Remove(existsTipoPalete);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<TipoPalete> Add(TipoPalete tipoPalete)
    {
        _dbContext.TipoPaletes.Add(tipoPalete);
        await _dbContext.SaveChangesAsync();
        return tipoPalete;
    }

    public async Task<TipoPalete?> Update(Guid id, TipoPalete tipoPalete)
    {
        var existsTipoPalete =  await GetById(id);
        
        if (existsTipoPalete == null)
            return null;
        
        existsTipoPalete.Nome = tipoPalete.Nome;
        existsTipoPalete.AlturaMaximo = tipoPalete.AlturaMaximo;
        existsTipoPalete.PesoMaximo = tipoPalete.PesoMaximo;
        existsTipoPalete.VolumeMaximo = tipoPalete.VolumeMaximo;
        
        
        await _dbContext.SaveChangesAsync();
        return existsTipoPalete;
    }
}