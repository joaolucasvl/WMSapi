using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories;

public interface ITipoPaleteRepository
{
    Task <IEnumerable<TipoPalete>> GetAll();
    Task <TipoPalete?> GetById(Guid id);
    Task <bool> Delete(Guid id);
    Task <TipoPalete> Add(TipoPalete tipoPalete);
    Task<TipoPalete?> Update(Guid id,  TipoPalete tipoPalete);
}