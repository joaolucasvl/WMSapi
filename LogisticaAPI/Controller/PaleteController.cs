using LogisticaAPI.DTOs;
using LogisticaAPI.Entities;
using LogisticaAPI.Repositories;
using LogisticaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaAPI.Controller;


[Route("api/[controller]/[action]")]
[ApiController]
public class PaleteController : ControllerBase
{
    private readonly IPaleteService _paleteService;
    
    public PaleteController(IPaleteService paleteService)
    {
        _paleteService = paleteService;
    }

    private static AlocacaoResponseDto MapToResponse(ItemPalete a) => new()
    {
        ItemPaleteId = a.ItemPaleteId,
        PaleteId = a.PaleteId,
        ItemPedidoId = a.ItemPedidoId,
        Quantidade = a.Quantidade
    };

    private static PaleteResponseDto MapToPaleteResponse(Palete p) => new()
    {
        PaleteId = p.PaleteId,
        Numero = p.Numero,
        PesoMaximo =  p.PesoMaximo,
        VolumeMaximo =   p.VolumeMaximo,
        AlturaMaxima =  p.AlturaMaxima,
        PesoAtual =  p.PesoAtual,
        VolumeAtual =  p.VolumeAtual,
        CarregamentoId = p.CarregamentoId,
    };

    [HttpPost]
    public async Task<ActionResult<PaleteResponseDto>> CriarPalete(PaleteRequestDto request)
    {
        var palete = await _paleteService.CriarPalete(request);
        return Ok(MapToPaleteResponse(palete));
    }


    [HttpPost("{paleteId}")]
    public async Task<ActionResult<AlocacaoResponseDto>> Alocar(int paleteId, AlocacaoRequestDto request)
    {
        var alocacao = await _paleteService.Alocar(paleteId, request);
        return Ok(MapToResponse(alocacao));
    }

    [HttpDelete("{paleteId}")]
    public async Task<ActionResult> Desalocar(int paleteId)
    {
        await _paleteService.Desalocar(paleteId);
        return NoContent();
    }
}