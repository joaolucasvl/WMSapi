namespace LogisticaAPI.Entities.Autenticacao;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; }
    public string Perfil { get; set; } = "User";
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

}