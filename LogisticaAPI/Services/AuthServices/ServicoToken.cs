using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LogisticaAPI.Common;
using LogisticaAPI.Entities.Autenticacao;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace LogisticaAPI.Services.AuthServices;

public class ServicoToken : IServicoToken
{
    private readonly JwtOptions _opcoes;

    public ServicoToken(IOptions<JwtOptions> opcoes)
    {
        _opcoes = opcoes.Value;
    }

    public (string Token, DateTime ExpiraEm) GerarAccessToken(Usuario usuario)
    {
        var expiraEm = DateTime.UtcNow.AddMinutes(_opcoes.AccessTokenMinutos);

        var descritor = new SecurityTokenDescriptor
        {
            Issuer = _opcoes.Issuer,
            Audience = _opcoes.Audience,
            Expires = expiraEm,
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("name", usuario.Nome),
                new Claim("role", usuario.Perfil)
            ]),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opcoes.Chave)),
                SecurityAlgorithms.HmacSha256)
        };

        return (new JsonWebTokenHandler().CreateToken(descritor), expiraEm);
    }

    public string GerarRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
