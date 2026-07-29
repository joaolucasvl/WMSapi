namespace LogisticaAPI.Repositories.AuthRepositories;

public interface IUnitOfWork
{
    IUsuarioRepository Usuario { get; }
    IRefreshTokenRepository RefreshToken { get; }
    Task CommitAsync();
}