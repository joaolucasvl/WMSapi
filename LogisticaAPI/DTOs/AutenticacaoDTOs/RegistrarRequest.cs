using System.ComponentModel.DataAnnotations;

namespace LogisticaAPI.DTOs.AutenticacaoDTOs;

public class RegistrarRequest
{
    
    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; }
    
    [Required]
    [MinLength(8)]
    [MaxLength(100)]
    public string Senha { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string Nome { get; set; }
    
}