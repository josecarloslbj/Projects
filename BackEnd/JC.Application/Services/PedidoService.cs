using AutoMapper;
using JC.Application.Exceptions;
using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using System.Diagnostics.CodeAnalysis;
using JC.Core.Comuns;

namespace JC.Application.Services 
{
    [ExcludeFromCodeCoverageAttribute]
    public class PedidoService : IPedidoService
    {
        private readonly IMapper _mapper;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IEstoqueProdutoService _estoqueProdutoService;
        public PedidoService(IMapper mapper,
            IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository,
            IEstoqueProdutoService estoqueProdutoRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _estoqueProdutoService = estoqueProdutoRepository;
        }


        public async Task<PedidoDTO> Obter(int id)
        {
            return _mapper.Map<PedidoDTO>(await _pedidoRepository.Obter(id));
        }

        public async Task<PedidoDTO> Criar(PedidoDTO record)
        {
            record.ValorTotal = record.ValorPedido.GetValueOrDefault(0) - record.ValorDesconto.GetValueOrDefault(0) + record.ValorAcrescimo.GetValueOrDefault(0);
            record.SituacaoAtual = record.Id > 0 ? record.SituacaoAtual : SituacaoPedido.CRIADO;
            record.DataCadastro = DateTime.Now;

            var pedido = await _pedidoRepository.Criar(_mapper.Map<Pedido>(record));
            if (record.ItensPedido != null)
            {
                foreach (var item in record.ItensPedido)
                {
                    var itemPedido = _mapper.Map<ItemPedido>(item);

                    itemPedido.SituacaoAtual = SituacaoPedido.CRIADO;
                    itemPedido.IdPedido = pedido.Id;

                    itemPedido.ValorTotal = itemPedido.ValorUnitario - itemPedido.ValorDesconto.GetValueOrDefault(0) + itemPedido.ValorAcrescimo.GetValueOrDefault(0);

                    var it = await _itemPedidoRepository.SaveOrUpdate(itemPedido);
                    item.Id = it.Id;


                    await _estoqueProdutoService.RealizarBaixaEstoque(item);
                }
            }
            else
                throw new DomainLayerException("Pedido não possui items.");

            record.Id = pedido.Id;
            return record;
        }

        public async Task<PagedResultDTO<PedidoDTO>> ObterPaginado(FilterDTO filters)
        {
            var retorno = await _pedidoRepository.ObterPaginado(filters);
            return _mapper.Map<PagedResultDTO<PedidoDTO>>(retorno);
        }



    }
}
