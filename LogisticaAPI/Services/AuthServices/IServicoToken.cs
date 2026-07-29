using LogisticaAPI.Entities.Autenticacao;

namespace LogisticaAPI.Services.AuthServices;

public interface IServicoToken
{
    (string Token, DateTime ExpiraEm) GerarAccessToken(Usuario usuario);
    string GerarRefreshToken();
}
