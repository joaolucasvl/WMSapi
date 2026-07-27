using LogisticaAPI.DTOs;
using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;

namespace LogisticaAPI.Services.PedidoServices;

public interface IPedidoService
{
    Task<PagedResult<Pedido>> GetPaged(QueryableParameters parametros);
    Task<Pedido?> GetById(int id);
    Task<Pedido> Create(PedidoRequestDto request);
    Task<Pedido?> Update(int id, PedidoRequestDto request);
    Task<bool> Delete(int id);
}