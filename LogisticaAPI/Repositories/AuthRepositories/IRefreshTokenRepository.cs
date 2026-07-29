using LogisticaAPI.Entities.Autenticacao;

namespace LogisticaAPI.Repositories.AuthRepositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> ObterPorTokenAsync(string token);
    Task<RefreshToken?> ObterDoUsuarioAsync(string token, int usuarioId);
    void Adicionar(RefreshToken token);
    Task RevogarTodosDoUsuarioAsync(int usuarioId);
}