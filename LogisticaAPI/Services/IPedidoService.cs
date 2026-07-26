using LogisticaAPI.DTOs;
using LogisticaAPI.Entities;

namespace LogisticaAPI.Services;

public interface IPedidoService
{
    Task<IEnumerable<Pedido?>> GetAll();
    Task<Pedido?> GetById(int id);
    Task<Pedido> Create(PedidoRequestDto request);
    Task<Pedido?> Update(int id, PedidoRequestDto request);
    Task<bool> Delete(int id);
}