using System.ComponentModel.DataAnnotations;

namespace LogisticaAPI.DTOs.AutenticacaoDTOs;

public class RefreshRequest
{
    [Required]
    public string RefreshToken { get; set; }
}