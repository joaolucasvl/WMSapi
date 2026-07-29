namespace LogisticaAPI.DTOs.AutenticacaoDTOs;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiraEm { get; set; }
}