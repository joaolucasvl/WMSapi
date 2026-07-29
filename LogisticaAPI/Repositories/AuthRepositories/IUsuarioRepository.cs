using LogisticaAPI.Entities.Autenticacao;

namespace LogisticaAPI.Repositories.AuthRepositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string Email);
    Task<Usuario?> ObterPorIdAsync(int id);
    void AdicionarUsuario(Usuario usuario);
}