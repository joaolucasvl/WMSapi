using LogisticaAPI.Entities.Autenticacao;
using Microsoft.AspNetCore.Identity;

namespace LogisticaAPI.Services.AuthServices;

public class ServicoSenha : IServicoSenha
{
    private readonly PasswordHasher<Usuario> _hasher = new();
    private static readonly Usuario Dummy = new();
     
    public string Hash(string senha)
    {
        return _hasher.HashPassword(Dummy, senha);
    }

    public bool Verificar(string senha, string hash)
    {
        return _hasher.VerifyHashedPassword(Dummy, hash, senha) 
            != PasswordVerificationResult.Failed;
    }
}