namespace LogisticaAPI.Exceptions;

public class PaleteSemCapacidadeException : Exception
{
    public PaleteSemCapacidadeException(int paleteId, string limite)
        : base($"O palete {paleteId} nao tem capacidade de {limite} para esta alocacao.")
    { }
}