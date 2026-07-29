using LogisticaAPI.Common;
using LogisticaAPI.DTOs.AutenticacaoDTOs;
using LogisticaAPI.Entities.Autenticacao;

namespace LogisticaAPI.Services.AuthServices;

public interface IServicoAuth
{
    Task<Resultado<Usuario>> RegistrarAsync(RegistrarRequest request);
    Task<Resultado<TokenResponse>> LoginAsync(LoginRequest request);
    Task<Resultado<TokenResponse>> AtualizarAsync(RefreshRequest request);
    Task<Resultado<bool>> LogoutAsync(int usuarioId);
}
