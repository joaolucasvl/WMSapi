using LogisticaAPI.Entities;

namespace LogisticaAPI.Exceptions;

public class TipoPaleteNaoEncontradoException : Exception
{
    public Guid TipoPaleteId { get; set; }

    public TipoPaleteNaoEncontradoException(Guid tipoPalete) : base(
        $"Tipo de palete {tipoPalete} não encontrado")
    {
        TipoPaleteId = tipoPalete;
    }
}