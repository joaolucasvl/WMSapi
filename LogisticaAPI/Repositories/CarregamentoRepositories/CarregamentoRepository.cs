using LogisticaAPI.Data;
using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.Repositories.CarregamentoRepositories;

public class CarregamentoRepository : ICarregamentoRepository
{
    private AppDbContext _dbContext;

    public CarregamentoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Carregamento?> GetbyId(Guid id)
    {
        return await _dbContext.Carregamentos.FirstOrDefaultAsync(c => c.CarregamentoId == id);
    }

    public async Task<PagedResult<Carregamento>> GetAll(QueryableParameters parametros)
    {
        return await _dbContext.Carregamentos.
                                AsNoTracking()
                                .OrderByDescending(c => c.CriadoEm)
                                .ThenBy(c => c.CarregamentoId)
                                .ToPagedResultAsync(parametros);
    }

    public async Task<bool> Delete(Guid id)
    {
        var carregamento = await GetbyId(id);
        if (carregamento is null)
            return  false; 
        
        _dbContext.Carregamentos.Remove(carregamento);
        await _dbContext.SaveChangesAsync();
        return true;
         
    }

    public async Task<Carregamento> Add(Carregamento carregamento)
    {
        _dbContext.Carregamentos.Add(carregamento);
        await _dbContext.SaveChangesAsync();
        return carregamento;
    }

    public async Task<Carregamento?> Update(Guid id, Carregamento carregamento)
    {
        var existCarregamento = await GetbyId(id);
        if (existCarregamento is null)
            return null;
        
        existCarregamento.DataDeChegada = carregamento.DataDeChegada;
        existCarregamento.ModeloCaminhao = carregamento.ModeloCaminhao;
        existCarregamento.PesoTotal = carregamento.PesoTotal;
        existCarregamento.Rota = carregamento.Rota;
        existCarregamento.Transportadora = carregamento.Transportadora;
        existCarregamento.DataDeCarregamento = carregamento.DataDeCarregamento;
        
        await _dbContext.SaveChangesAsync();
        
        return existCarregamento;
        
    }
}