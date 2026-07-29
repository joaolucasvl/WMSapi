using System.ComponentModel.DataAnnotations;

namespace LogisticaAPI.DTOs.AutenticacaoDTOs;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Senha { get; set; }
}