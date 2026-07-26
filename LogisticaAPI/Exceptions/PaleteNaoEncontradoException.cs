using LogisticaAPI.Entities;

namespace LogisticaAPI.Exceptions;

public class PaleteNaoEncontradoException : Exception
{
    public int PaleteId { get; set; }
    
    public PaleteNaoEncontradoException(int paleteId) : base($"Palete {paleteId} não encontrado.")
    {
        PaleteId = paleteId;
    }
}