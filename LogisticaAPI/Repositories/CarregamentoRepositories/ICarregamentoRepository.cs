using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;

namespace LogisticaAPI.Repositories.CarregamentoRepositories;

public interface ICarregamentoRepository
{
    Task <Carregamento?> GetbyId(Guid id);
    Task<PagedResult<Carregamento>> GetAll(QueryableParameters parametros);
    Task <bool> Delete(Guid id);
    Task <Carregamento> Add(Carregamento carregamento);
    Task <Carregamento?> Update(Guid id,Carregamento carregamento);
}