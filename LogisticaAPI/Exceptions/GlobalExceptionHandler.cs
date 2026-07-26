using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaAPI.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, Exception exception, CancellationToken ct)
    {
        var (status, titulo) = exception switch
        {
            ItemNaoEcontradoException => (StatusCodes.Status400BadRequest, "Item invalido"),
            PaleteNaoEncontradoException => (StatusCodes.Status404NotFound, "Palete nao encontrado"),
            TipoPaleteNaoEncontradoException => (StatusCodes.Status404NotFound, "Tipo de Palete nao encontrado"),
            CarregamentoNaoEncontradoException => (StatusCodes.Status404NotFound, "Carregamento nao encontrado"),
            AlocacaoNaoEncontradaException => (StatusCodes.Status404NotFound, "Alocacao nao encontrado"),
            QuantidadeExcedeSaldoException => (StatusCodes.Status409Conflict, "Saldo insuficiente"),
            PaleteSemCapacidadeException => (StatusCodes.Status409Conflict, "Palete sem capacidade"),
            _ => (StatusCodes.Status500InternalServerError, "Erro interno")
        };

        var problem = new ProblemDetails
        {
            Status = status,
            Title  = titulo,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(problem, ct);
        return true;   
    }
}