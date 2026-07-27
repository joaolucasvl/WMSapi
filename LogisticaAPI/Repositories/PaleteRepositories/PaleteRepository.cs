using LogisticaAPI.Data;
using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.Repositories.PaleteRepositories;

public class PaleteRepository : IPaleteRepository
{
    private readonly AppDbContext _dbContext;
    
    public PaleteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<Palete?> GetbyId(int id)
    {
        return await _dbContext.Paletes.Include(p => p.Itens).FirstOrDefaultAsync(p => p.PaleteId == id);
    }

    public async Task<PagedResult<Palete>> GetPaged(QueryableParameters parametros)
    {
        return await _dbContext.Paletes
                                .Include(p => p.Itens)
                                .AsNoTracking()
                                .OrderByDescending(p => p.PaleteId)
                                .AsSplitQuery()
                                .ToPagedResultAsync(parametros);
    }


    public async Task<bool> Delete(int id)
    {
        var palete = await GetbyId(id);
        
        if (palete is null)
            return false;
        
        _dbContext.Paletes.Remove(palete);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Palete> Add(Palete palete)
    {
        _dbContext.Paletes.Add(palete);
        await _dbContext.SaveChangesAsync();
        return palete;
    }

    public async Task<Palete?> Update(int id, Palete pedido)
    {
        var p = await GetbyId(id);
        
        if(p is null)
            return  null;

        p.Numero = pedido.Numero;
        p.AlturaMaxima = pedido.AlturaMaxima;
        p.PesoAtual = pedido.PesoAtual;
        p.VolumeAtual = pedido.VolumeAtual;
        p.VolumeMaximo = pedido.VolumeMaximo;
        p.Itens = pedido.Itens;
        
        await _dbContext.SaveChangesAsync();
        return p;
    }

    public async Task<ItemPalete> AdicionarAlocacao(ItemPalete alocacao)
    {
        _dbContext.ItensPalete.Add(alocacao);
        await _dbContext.SaveChangesAsync();
        return alocacao;
    }

    public async Task<ItemPalete> RemoverAlocacao(ItemPalete alocacao)
    {
        _dbContext.ItensPalete.Remove(alocacao);
        await _dbContext.SaveChangesAsync();
        return alocacao;
    }

    public async Task<ItemPalete?> GetAlocacaoById(int id)
    {
        return await _dbContext.ItensPalete
            .Include(i => i.Palete)    
            .Include(i => i.ItemPedido)
            .FirstOrDefaultAsync(p => p.ItemPaleteId == id);
    }
}