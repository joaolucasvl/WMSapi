using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories;

public interface IPedidoRepository
{
    Task <Pedido?> GetbyId(int id);
    Task<IEnumerable<Pedido?>> GetAll();
    Task <bool> Delete(int id);
    Task <Pedido> Add(Pedido pedido);
    Task <Pedido?> Update(int id,Pedido pedido);   
    Task<ItemPedido?> GetItemPedido(int id);
}