using LogisticaAPI.Data;

namespace LogisticaAPI.Repositories.AuthRepositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext, IUsuarioRepository usuario, IRefreshTokenRepository refreshToken)
    {
        _dbContext = dbContext;
        Usuario = usuario;
        RefreshToken = refreshToken;
    }

    public IUsuarioRepository Usuario { get; }
    public IRefreshTokenRepository RefreshToken { get; }

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
