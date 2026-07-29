namespace LogisticaAPI.Entities.Autenticacao;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiraEm { get; set; }
    
    public DateTime? RevogadoEm { get; set; }
    public string? SubstituidoPor { get; set; }
    
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public bool Ativo => RevogadoEm is null && DateTime.UtcNow < ExpiraEm;
}