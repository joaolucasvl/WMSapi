
using LogisticaAPI.DTOs;
using LogisticaAPI.Entities;
using LogisticaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LogisticaAPI.Controller;


[Route("api/[controller]/[action]")]
[ApiController]
public class LogisticaController : ControllerBase
{
    private readonly ICarregamentoRepository _repository;

    public LogisticaController(ICarregamentoRepository repository)
    {
        _repository = repository;
    }

    private static CarregamentoResponseDto MapToResponse(Carregamento c)
    {
        return new CarregamentoResponseDto
        {
            CarregamentoId = c.CarregamentoId,
            CriadoEm = c.CriadoEm,
            Transportadora = c.Transportadora,
            Rota = c.Rota,
            Status = c.Status,
            DataDeCarregamento = c.DataDeCarregamento,
            DataDeChegada = c.DataDeChegada,
            PesoTotal = c.PesoTotal,
            ModeloCaminhao = c.ModeloCaminhao,
        };
    }
    

    [HttpGet]
    public async Task<IEnumerable<CarregamentoResponseDto>> Get()
    {
        var carregamentos =  await _repository.GetAll();
        return carregamentos.Where(c => c is not null).Select(c => MapToResponse(c!));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarregamentoResponseDto>> Get(Guid id)
    {
        var carregamento = await _repository.GetbyId(id);
        
        if (carregamento == null)
            return NotFound();
        
        return MapToResponse(carregamento);
    }

    [HttpPost]
    public async Task<ActionResult<CarregamentoResponseDto>> Create(CarregamentoRequestDto request)
    {
        var carregamento = new Carregamento
        {
            Transportadora = request.Transportadora,
            Rota = request.Rota,
            DataDeCarregamento = request.DataDeCarregamento,
            DataDeChegada = request.DataDeChegada,
            PesoTotal = request.PesoTotal,
            ModeloCaminhao = request.ModeloCaminhao,
            CriadoEm = DateTime.Now
        };

        var criado = await _repository.Add(carregamento);
        var response = MapToResponse(criado);
        
        return CreatedAtAction(nameof(Get), new { id = response.CarregamentoId }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CarregamentoResponseDto>> Update(Guid id, CarregamentoRequestDto request)
    {

        var carregamento = new Carregamento
        {
            Transportadora = request.Transportadora,
            Rota = request.Rota,
            DataDeCarregamento = request.DataDeCarregamento,
            DataDeChegada = request.DataDeChegada,
            PesoTotal = request.PesoTotal,
            ModeloCaminhao = request.ModeloCaminhao,
        };

        var atualizado = await _repository.Update(id, carregamento);
        if (atualizado is null)
            return NotFound();
        
        return Ok(MapToResponse(atualizado));

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var sucesso = await _repository.Delete(id);
        if (!sucesso)
            return NotFound();
        
        return NoContent();
    }
}