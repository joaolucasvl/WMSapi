using LogisticaAPI.DTOs;
using LogisticaAPI.Entities;
using LogisticaAPI.Exceptions;
using LogisticaAPI.Repositories;

namespace LogisticaAPI.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IItemRepository _itemRepository;

    public PedidoService(IPedidoRepository pedidoRepository, IItemRepository itemRepository)
    {
        _pedidoRepository = pedidoRepository;
        _itemRepository = itemRepository;
    }
    
    public async Task<Pedido> Create(PedidoRequestDto request)
    {
        var pedido = new Pedido
        {
            Cliente = request.Cliente,
            TipoPedido = request.TipoPedido,
            CriadoEm = DateTime.Now,
        };

        foreach (var dto in request.Itens)
        {
            var item = await _itemRepository.GetbyId(dto.ItemId);
            
            if (item is null)
                throw new ItemNaoEcontradoException(dto.ItemId);
            
            pedido.ItensPedido.Add(new ItemPedido
            {
                ItemId = item.ItemId,
                Quantidade = dto.Quantidade,
                Descricao = item.Descricao,
                PesoUnitario =  item.PesoUnitario,
                VolumeUnitario =   item.VolumeUnitario,
                AlturaUnitario =  item.AlturaUnitario,
            });
        }

        return await _pedidoRepository.Add(pedido);
    }
    
    
    public async Task<IEnumerable<Pedido?>> GetAll()
    {
        return await _pedidoRepository.GetAll();
    }

    public async Task<Pedido?> GetById(int id)
    {
        return await _pedidoRepository.GetbyId(id);
    }

    public async Task<Pedido?> Update(int id, PedidoRequestDto request)
    {
        var pedido = new Pedido
        {
            Cliente = request.Cliente,
            TipoPedido = request.TipoPedido
        };

        foreach (var dto in request.Itens)
        {
            var item = await _itemRepository.GetbyId(dto.ItemId);
            
            if (item is null)
                throw new ItemNaoEcontradoException(dto.ItemId);
            
            pedido.ItensPedido.Add(new ItemPedido
            {
                ItemPedidoId = dto.ItemPedidoId,
                ItemId = item.ItemId,
                Quantidade = dto.Quantidade,
                Descricao = item.Descricao,
                PesoUnitario =  item.PesoUnitario,
                VolumeUnitario =   item.VolumeUnitario,
                AlturaUnitario =  item.AlturaUnitario,
            });
        }

        return await _pedidoRepository.Update(id, pedido);
    }

    public async Task<bool> Delete(int id)
    {
        return await _pedidoRepository.Delete(id);
    }
}