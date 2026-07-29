using LogisticaAPI.Data;
using LogisticaAPI.Entities.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LogisticaAPI.Repositories.AuthRepositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _dbContext;
    public RefreshTokenRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RefreshToken?> ObterPorTokenAsync(string token)
    {
        return await  _dbContext.RefreshTokens
            .Include(r => r.Usuario)
            .SingleOrDefaultAsync(t => t.Token == token);
    }

    public async Task<RefreshToken?> ObterDoUsuarioAsync(string token, int usuarioId)
    {
        return await _dbContext.RefreshTokens
            .SingleOrDefaultAsync(t => t.Token == token && t.UsuarioId == usuarioId);
    }

    public void Adicionar(RefreshToken token)
    {
        _dbContext.RefreshTokens.Add(token);
        _dbContext.SaveChanges();
    }

    public async Task RevogarTodosDoUsuarioAsync(int usuarioId)
    {
        var ativos = await _dbContext.RefreshTokens
                                .Where(t => t.UsuarioId == usuarioId && t.RevogadoEm == null)
                                .ToListAsync();
        
        foreach (var ativo in ativos) 
            ativo.RevogadoEm = DateTime.Now;
            
    }
}