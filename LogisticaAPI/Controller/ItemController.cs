using LogisticaAPI.DTOs;
using LogisticaAPI.Entities;
using LogisticaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaAPI.Controller;


[Route("api/[controller]/[action]")]
[ApiController]
public class ItemController : ControllerBase
{
    
    private readonly IItemRepository _repository;
    
    public ItemController(IItemRepository repository)
    {
        _repository = repository;
    }


    private static ItemResponseDto MaptoResponse(Item item)
    {
        return new ItemResponseDto
        {
            ItemId =  item.ItemId,
            Nome = item.Nome,
            Descricao = item.Descricao,
            Perecivel = item.Perecivel,
            Fornecedor = item.Fornecedor,
            VolumeUnitario = item.VolumeUnitario,
            PesoUnitario = item.PesoUnitario,
            AlturaUnitario = item.AlturaUnitario,
        };
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemResponseDto>>> GetAll()
    {
        var itens = await _repository.GetAll();
        var response =  itens.Select(MaptoResponse).ToList();
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemResponseDto>> GetById(int id)
    {
        var item = await _repository.GetbyId(id);

        if (item is null)
            return NotFound();
        
        return MaptoResponse(item);
    }


    [HttpPost]
    public async Task<ActionResult<ItemResponseDto>> Create(ItemRequestDto request)
    {
        var newItem = new Item
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            Perecivel = request.Perecivel,
            Fornecedor = request.Fornecedor,
            VolumeUnitario = request.VolumeUnitario,
            PesoUnitario = request.PesoUnitario,
            AlturaUnitario = request.AlturaUnitario,

        };
        
        await _repository.Add(newItem);
        var response =  MaptoResponse(newItem);

        return CreatedAtAction(nameof(GetById), new { id = response.ItemId }, response);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<ItemResponseDto>> Update(int id, ItemRequestDto request)
    {
        var updateItem = new Item
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            Perecivel = request.Perecivel,
            Fornecedor = request.Fornecedor,
            VolumeUnitario = request.VolumeUnitario,
            PesoUnitario = request.PesoUnitario,
            AlturaUnitario = request.AlturaUnitario,

        };
        
        var atualizado = await  _repository.Update(id, updateItem);
        
        if (atualizado is null)
            return NotFound();
        
        var response =  MaptoResponse(atualizado);
        
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    { 
        var sucesso = await _repository.Delete(id);
        if (!sucesso)
            return NotFound();
        
        return NoContent();
    }
    
    
    
    
    
    
    
}