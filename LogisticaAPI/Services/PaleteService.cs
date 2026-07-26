using LogisticaAPI.DTOs;
using LogisticaAPI.Entities;
using LogisticaAPI.Exceptions;
using LogisticaAPI.Repositories;

namespace LogisticaAPI.Services;

public class PaleteService : IPaleteService
{
    
    private readonly IPaleteRepository _paleteRepository;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly ITipoPaleteRepository _tipoPaleteRepository;
    private readonly ICarregamentoRepository _carregamentoRepository;


    public PaleteService(IPaleteRepository paleteRepository, 
                            IPedidoRepository pedidoRepository,  
                            ITipoPaleteRepository tipoPaleteRepository,
                            ICarregamentoRepository carregamentoRepository)
    {
        _paleteRepository = paleteRepository;
        _pedidoRepository = pedidoRepository;
        _tipoPaleteRepository = tipoPaleteRepository;
        _carregamentoRepository = carregamentoRepository;
    }

    public async Task<ItemPalete> Alocar(int paleteId, AlocacaoRequestDto request)
    {

        var palete = await _paleteRepository.GetbyId(paleteId);
        if (palete is null)
            throw new PaleteNaoEncontradoException(paleteId);

        var itemPedido = await _pedidoRepository.GetItemPedido(request.ItemPedidoId);
        if (itemPedido is null)
            throw new ItemNaoEcontradoException(request.ItemPedidoId);

        

        var jaAlocado = itemPedido.Alocacoes.Sum(x => x.Quantidade);
        var saldo = itemPedido.Quantidade - jaAlocado;

        if (request.Quantidade <= 0 || request.Quantidade > saldo)
            throw new QuantidadeExcedeSaldoException(request.ItemPedidoId, request.Quantidade, saldo);
        

        var pesoDaAlocacao = itemPedido.PesoUnitario * request.Quantidade;
        var volumeDaAlocacao = itemPedido.VolumeUnitario * request.Quantidade;

        if (palete.PesoAtual + pesoDaAlocacao > palete.PesoMaximo)
            throw new PaleteSemCapacidadeException(paleteId, "peso");

        if (palete.VolumeAtual + volumeDaAlocacao > palete.VolumeMaximo)
            throw new PaleteSemCapacidadeException(paleteId, "volume");

        if (itemPedido.AlturaUnitario > palete.AlturaMaxima)
            throw new PaleteSemCapacidadeException(paleteId, "altura");
        

        var alocacao = new ItemPalete
        {
            PaleteId = paleteId,
            ItemPedidoId = request.ItemPedidoId,
            Quantidade = request.Quantidade,
        };

        palete.PesoAtual += pesoDaAlocacao;
        palete.VolumeAtual += volumeDaAlocacao;


        return await _paleteRepository.AdicionarAlocacao(alocacao);
    }

    public async Task<Palete> CriarPalete(PaleteRequestDto request)
    {

        var tipoPalete = await _tipoPaleteRepository.GetById(request.TipoPaleteId);

        if (tipoPalete is null)
            throw new TipoPaleteNaoEncontradoException(request.TipoPaleteId);

        if (request.CarregamentoId.HasValue)
        {
            var carregamento = await _carregamentoRepository.GetbyId(request.CarregamentoId.Value);
            if (carregamento is null)
                throw new CarregamentoNaoEncontradoException(request.CarregamentoId.Value);
        }
        

        var palete = new Palete
        {
            Numero = request.Numero,
            CarregamentoId = request.CarregamentoId,

            PesoMaximo = tipoPalete.PesoMaximo,
            VolumeMaximo = tipoPalete.VolumeMaximo,
            AlturaMaxima = tipoPalete.AlturaMaximo,

            PesoAtual = 0,
            VolumeAtual = 0,

        };
        
        return await _paleteRepository.Add(palete);
        
    }

    public async Task Desalocar(int itemPaleteId)
    {
        var alocacao = await _paleteRepository.GetAlocacaoById(itemPaleteId);
        
        if (alocacao is null)
            throw new AlocacaoNaoEncontradaException(itemPaleteId);

        var peso = alocacao.ItemPedido.PesoUnitario * alocacao.Quantidade;
        var volume = alocacao.ItemPedido.VolumeUnitario * alocacao.Quantidade;
        
        alocacao.Palete.PesoAtual -= peso;
        alocacao.Palete.VolumeAtual -= volume;
        
        await _paleteRepository.RemoverAlocacao(alocacao);

    }
}