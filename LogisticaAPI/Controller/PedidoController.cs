using LogisticaAPI.DTOs;
using LogisticaAPI.DTOs.Paginacao;
using LogisticaAPI.Entities;
using LogisticaAPI.Services.PedidoServices;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaAPI.Controller;


[Route("api/[controller]/[action]")]
[ApiController]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    
    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    private static PedidoResponseDto MapToResponse(Pedido p)
    {
        return new PedidoResponseDto
        {
            PedidoId =  p.PedidoId,
            TipoPedido =  p.TipoPedido,
            Cliente =   p.Cliente,
            CriadoEm =    p.CriadoEm,
            Itens = p.ItensPedido
                    .Select(MapItemToResponse)
                    .ToList()
        };
    }
    
    private static ItemPedidoResponseDto MapItemToResponse(ItemPedido i)
    {
        return new ItemPedidoResponseDto
        {
            ItemPedidoId   = i.ItemPedidoId,
            ItemId         = i.ItemId,
            Descricao      = i.Descricao,
            Quantidade     = i.Quantidade,
            PesoUnitario   = i.PesoUnitario,
            VolumeUnitario = i.VolumeUnitario,
            AlturaUnitario = i.AlturaUnitario,
            PesoTotal      = i.PesoUnitario   * i.Quantidade,
            VolumeTotal    = i.VolumeUnitario * i.Quantidade
        };
    }
    
    
    

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<PedidoResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<PedidoResponseDto>>> GetAll([FromQuery] QueryableParameters parametros)
    {
        var pagina = await _pedidoService.GetPaged(parametros);
        return Ok(pagina.Map(MapToResponse));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoResponseDto>> Get(int id)
    {
        var pedido = await _pedidoService.GetById(id);
        
        if (pedido == null) 
            return NotFound();
        
        var response = MapToResponse(pedido);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<PedidoResponseDto>> Create(PedidoRequestDto request)
    {
        
        var criado = await _pedidoService.Create(request);
        var response = MapToResponse(criado);
        return CreatedAtAction(nameof(Get), new { id = response.PedidoId }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PedidoResponseDto>> Update(int id, PedidoRequestDto request)
    {
        
        var atualizado = await _pedidoService.Update(id, request);
        
        if (atualizado == null)
            return NotFound();
        
        var response = MapToResponse(atualizado);
        return Ok(response);

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var sucesso = await _pedidoService.Delete(id);
        if (!sucesso)
            return  NotFound();
        
        return NoContent();
    }
    
}