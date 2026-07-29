namespace LogisticaAPI.Services.AuthServices;

public interface IServicoSenha
{
    public string Hash(string senha);
    public bool Verificar(string senha, string hash);
}