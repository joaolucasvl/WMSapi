using LogisticaAPI.Data;
using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.Repositories.PedidoRepositories;

public class PedidoRepository : IPedidoRepository
{
    
    private AppDbContext _dbContext;
    
    public PedidoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Pedido?> GetbyId(int id)
    {
        return await _dbContext.Pedidos.Include(p => p.ItensPedido).FirstOrDefaultAsync(p => p.PedidoId == id);
    }

    public async Task<PagedResult<Pedido>> GetPaged(QueryableParameters parametros)
    {
        return await _dbContext.Pedidos 
            .AsNoTracking()    
            .Include(p => p.ItensPedido)
            .AsSplitQuery()
            .OrderByDescending(p => p.CriadoEm)
            .ThenBy(p => p.PedidoId)
            .ToPagedResultAsync(parametros);
    }
    

    public async Task<bool> Delete(int id)
    {
        var pedido = await GetbyId(id);
        if (pedido == null)
            return false;
        
        _dbContext.Pedidos.Remove(pedido);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Pedido> Add(Pedido pedido)
    {
        _dbContext.Pedidos.Add(pedido);
        await _dbContext.SaveChangesAsync();
        return pedido;
    }

    public async Task<Pedido?> Update(int id, Pedido pedidoAtualizado)
    {
        var existsPedido = await _dbContext.Pedidos
                .Include(p => p.ItensPedido)
                .FirstOrDefaultAsync(p => p.PedidoId == id);
        
        
        if (existsPedido == null)
            return null;


        existsPedido.Cliente = pedidoAtualizado.Cliente;
        existsPedido.TipoPedido = pedidoAtualizado.TipoPedido;

        var novos = pedidoAtualizado.ItensPedido;
        var idsNoRequest = novos.Where(i => i.ItemPedidoId != 0)
            .Select(i => i.ItemPedidoId)
            .ToHashSet();
        
        
        var removidos = existsPedido.ItensPedido
            .Where(i => !idsNoRequest.Contains(i.ItemPedidoId))
            .ToList();
        _dbContext.ItensPedido.RemoveRange(removidos);

        foreach (var novo in novos.Where(i => i.ItemPedidoId != 0))
        {
            var existente = existsPedido.ItensPedido.FirstOrDefault(i => i.ItemPedidoId == novo.ItemPedidoId);
            if (existente is not null)
            {
                existente.Quantidade = novo.Quantidade;
                existente.ItemId = novo.ItemId;
            }
        }
        
        foreach (var novo in novos.Where(i => i.ItemPedidoId == 0))
            existsPedido.ItensPedido.Add(new ItemPedido
            {
                ItemId = novo.ItemId,
                Quantidade = novo.Quantidade,
                Descricao = novo.Descricao,       
                PesoUnitario = novo.PesoUnitario,    
                VolumeUnitario = novo.VolumeUnitario,  
                AlturaUnitario = novo.AlturaUnitario
            });
        
        
        await  _dbContext.SaveChangesAsync();
        return existsPedido;
    }

    public async Task<ItemPedido?> GetItemPedido(int id)
    {
        return await _dbContext.ItensPedido
            .Include(i => i.Alocacoes)
            .FirstOrDefaultAsync(i => i.ItemPedidoId == id);
    }
}