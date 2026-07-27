using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories.PedidoRepositories;

public interface IPedidoRepository
{
    Task <Pedido?> GetbyId(int id);
    Task<PagedResult<Pedido>> GetPaged(QueryableParameters parametros);
    Task <bool> Delete(int id);
    Task <Pedido> Add(Pedido pedido);
    Task <Pedido?> Update(int id,Pedido pedido);   
    Task<ItemPedido?> GetItemPedido(int id);
}