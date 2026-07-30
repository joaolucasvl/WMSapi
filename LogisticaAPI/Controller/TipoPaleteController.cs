
using LogisticaAPI.DTOs;
using LogisticaAPI.Entities;
using LogisticaAPI.Repositories.PaleteRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaAPI.Controller;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class TipoPaleteController : ControllerBase
{
    private ITipoPaleteRepository _repository;

    public TipoPaleteController(ITipoPaleteRepository repository)
    {
        _repository = repository;
    }

    private static TipoPaleteResponseDto MapToResponse(TipoPalete tp)
    {
        return new TipoPaleteResponseDto
        {
            TipoPaleteId =  tp.TipoPaleteId,
            Nome = tp.Nome,
            AlturaMaximo = tp.AlturaMaximo,
            PesoMaximo = tp.PesoMaximo,
            VolumeMaximo = tp.VolumeMaximo,
        };
    }
    
    [HttpGet]
    public async Task<IEnumerable<TipoPaleteResponseDto>> Get()
    {
        var tipoPaletes = await _repository.GetAll();
        return tipoPaletes.Where(t => t is not null).Select(t => MapToResponse(t!));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TipoPaleteResponseDto>> Get(Guid id)
    {
        var tipoPalete = await _repository.GetById(id);
        
        if(tipoPalete is null)
            return  NotFound();
        
        return MapToResponse(tipoPalete);
    }

    [HttpPost]
    public async Task<ActionResult<TipoPaleteResponseDto>> Create(TipoPaleteRequestDto request)
    {
        var tipoPalete = new TipoPalete
        {
            Nome = request.Nome,
            AlturaMaximo = request.AlturaMaximo,
            PesoMaximo = request.PesoMaximo,
            VolumeMaximo = request.VolumeMaximo,
        };
        
        var criado = await _repository.Add(tipoPalete);    
        var response = MapToResponse(criado);
        
        return CreatedAtAction(nameof(Get), new { id = response.TipoPaleteId }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TipoPaleteResponseDto?>> Update(Guid id, TipoPaleteRequestDto request)
    {
        var newTipoPalete = new TipoPalete
        {
            Nome = request.Nome,
            AlturaMaximo = request.AlturaMaximo,
            PesoMaximo = request.PesoMaximo,
            VolumeMaximo = request.VolumeMaximo,
        };
        
        var atualizado = await _repository.Update(id, newTipoPalete);
        
        if (atualizado is null)
            return  NotFound();
        
        return Ok(MapToResponse(atualizado));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var sucesso = await _repository.Delete(id);
        
        if(!sucesso)
            return  NotFound();
        
        return NoContent();
    }

    
}