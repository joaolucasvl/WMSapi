using LogisticaAPI.Data;
using LogisticaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.Repositories;

public class ItemRepository : IItemRepository
{
    private AppDbContext _dbContext;

    public ItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Item?> GetbyId(int id)
    {
        return await _dbContext.Itens.FirstOrDefaultAsync(i => i.ItemId == id);
    }

    public async Task<IEnumerable<Item?>> GetAll()
    {
        var itens = await _dbContext.Itens.AsNoTracking().ToListAsync();
        return itens;
    }

    public async Task<bool> Delete(int id)
    {
        var item = await GetbyId(id);
        if (item is null)
            return false;
            
        _dbContext.Itens.Remove(item);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Item> Add(Item item)
    {
        _dbContext.Itens.Add(item);
        await _dbContext.SaveChangesAsync();
        
        return item;
    }

    public async Task<Item?> Update(int id, Item item)
    {
        var existItem = await GetbyId(id);
        if (existItem is null)
            return null;
        
        existItem.Nome = item.Nome;
        existItem.Descricao = item.Descricao;
        existItem.Fornecedor = item.Fornecedor;
        existItem.AlturaUnitario = item.AlturaUnitario;
        existItem.PesoUnitario = item.PesoUnitario;
        existItem.VolumeUnitario = item.VolumeUnitario;
        existItem.Perecivel = item.Perecivel;
        
        await _dbContext.SaveChangesAsync();
        return existItem;
        
    }
    
    
}