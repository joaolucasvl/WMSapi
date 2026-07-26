using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories;

public interface ICarregamentoRepository
{
    Task <Carregamento?> GetbyId(Guid id);
    Task<IEnumerable<Carregamento?>> GetAll();
    Task <bool> Delete(Guid id);
    Task <Carregamento> Add(Carregamento carregamento);
    Task <Carregamento?> Update(Guid id,Carregamento carregamento);
}