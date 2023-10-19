using AutoMapper;
using JC.Application.Exceptions;
using JC.Core.Dtos;
using JC.Core.Entities;
using JC.Core.Repositories;

namespace JC.Application.Services
{
    public class EstoqueProdutoService : IEstoqueProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IEstoqueProdutoRepository _estoqueProdutoRepository;
        private readonly IProdutoService _produtoService;
        public EstoqueProdutoService(IMapper mapper,
            IEstoqueProdutoRepository estoqueProdutoRepository,
            IProdutoService produtoService)
        {
            _mapper = mapper;
            _estoqueProdutoRepository = estoqueProdutoRepository;
            _produtoService = produtoService;
        }

        public async Task<EstoqueProdutoDTO?> RealizarBaixaEstoque(ItemPedidoDTO itemPedido)
        {
            if (itemPedido == null || !itemPedido.IdProduto.HasValue)
                throw new DomainLayerException($"Não foi possível realizar a baixa no Estoque, itemPedido NULO");

            int idProduto = itemPedido.IdProduto.Value;
            int qtde = (int)itemPedido.Quantidade.GetValueOrDefault(0);

            var produto = await _produtoService.Obter(idProduto);
            if (produto == null)
                throw new DomainLayerException($"Não foi possível localizar o produto com o Id.{idProduto}");

            produto.Qtde = produto.Qtde - qtde;
            await _produtoService.Salvar(produto);

            EstoqueProduto? estoque = null;

            var estoqueProduto = await _estoqueProdutoRepository.SelectAsync(q => q.IdProduto == idProduto);
            if (estoqueProduto == null || !estoqueProduto.Any())
            {
                estoque = new EstoqueProduto();
                estoque.QtdeAtual = produto.Qtde;
            }
            else
            {
                estoque = estoqueProduto.FirstOrDefault();
                if (estoque == null) return null;

                estoque.QtdeAtual = estoque.QtdeAtual - qtde;
            }

            estoque.IdProduto = idProduto;
            estoque.IdUsuario = Contexto.Atual?.IdUsuario;
            estoque.Ativo = true;
            estoque.Observacao = $"Baixa estoque realizado na venda Id:{itemPedido.IdPedido}";

            var retorno = await _estoqueProdutoRepository.SaveOrUpdate(estoque);
            return _mapper.Map<EstoqueProdutoDTO>(retorno);
        }

        public async Task<EstoqueProdutoDTO> Salvar(EstoqueProdutoDTO record)
        {
            var retorno = await _estoqueProdutoRepository.SaveOrUpdate(_mapper.Map<EstoqueProduto>(record));

            return _mapper.Map<EstoqueProdutoDTO>(retorno);
        }
    }
}
