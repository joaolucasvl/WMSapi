using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories;

public interface IPaleteRepository
{
    Task <Palete?> GetbyId(int id);
    Task<IEnumerable<Palete?>> GetAll();
    Task <bool> Delete(int id);
    Task <Palete> Add(Palete palete);
    Task <Palete?> Update(int id,Palete pedido);  
    Task<ItemPalete> AdicionarAlocacao(ItemPalete alocacao);
    Task<ItemPalete> RemoverAlocacao(ItemPalete alocacao);
    Task<ItemPalete?> GetAlocacaoById(int id);
}