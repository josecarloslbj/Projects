using AutoMapper;
using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Domain.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services 
{
    [ExcludeFromCodeCoverageAttribute]
    public class ProdutoService : IProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IArquivoRepository _arquivoRepository;
        public ProdutoService(IMapper mapper,
            IProdutoRepository produtoRepository, IArquivoRepository arquivoRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _arquivoRepository = arquivoRepository;
        }

        public async Task Deletar(int id)
        {
            await _produtoRepository.Deletar(id);
        }

        public async Task<ProdutoDTO> Obter(int id)
        {
            var retorno = await _produtoRepository.Obter(id);
            return _mapper.Map<ProdutoDTO>(retorno);
        }

        public async Task<PagedResultDTO<ProdutoDTO>> ObterPaginado(ProdutoFilterDTO filters)
        {
            var retorno = await _produtoRepository.ObterPaginado(filters);
            return _mapper.Map<PagedResultDTO<ProdutoDTO>>(retorno);
        }

        public async Task<ProdutoDTO> Salvar(ProdutoDTO produto)
        {
            produto.IdUsuario = Contexto.Atual?.IdUsuario;
            if (produto.IdArquivo == 0)
                produto.IdArquivo = null;

            if (produto.IdGrupo == 0)
                produto.IdGrupo = null;

            if (produto.IdFornecedor == 0)
                produto.IdFornecedor = null;

            if (produto.IdFabricante == 0)
                produto.IdFabricante = null;

            if (produto.Id == 0)
                produto.DataCadastro = DateTime.Now;
            else
                produto.DataAlteracao = DateTime.Now;

            var produtoModel = await _produtoRepository.Salvar(_mapper.Map<Produto>(produto));

            if (produto.IdArquivo.GetValueOrDefault(0) > 0)
            {
                var arquivo = await _arquivoRepository.ObterPorId(produto.IdArquivo.GetValueOrDefault(0));
                if (arquivo != null)
                {
                    arquivo.Entidade = produtoModel.GetType().Name;
                    arquivo.IdEntidade = produtoModel.Id;

                    await _arquivoRepository.Salvar(arquivo, true);
                }
            }
            else
            {
                if (produtoModel.IsEdicao)
                {
                    var existeArquivo = await _arquivoRepository.SelectAsync(q => q.Entidade == "produto" && q.IdEntidade == produtoModel.Id);
                    if (existeArquivo != null)
                    {
                        foreach (var item in existeArquivo)
                            await _arquivoRepository.DeleteAsync(item.Id);
                    }
                }
            }

            return _mapper.Map<ProdutoDTO>(produtoModel);
        }

        public async Task<ProdutoDTO> ObterPorCodigoBarras(string codigoBarras)
        {
            var retorno = await _produtoRepository.SelectAsync(q => q.Codigo == codigoBarras);

            var obj = _mapper.Map<ProdutoDTO>(retorno.Any() ? retorno.FirstOrDefault() : new ProdutoDTO());

            if (obj.IdArquivo.HasValue)
            {
                var arquivo = await _arquivoRepository.Get(obj.IdArquivo.Value);

                if (arquivo != null)
                {
                    obj.Arquivo = _mapper.Map<ArquivoDTO>(arquivo);
                }
            }
            return obj;
        }
    }
}
