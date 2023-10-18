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
    public class GrupoProdutoService : IGrupoProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IGrupoProdutoRepository _grupoProdutoRepository;
        public GrupoProdutoService(IMapper mapper,
            IGrupoProdutoRepository grupoProdutoRepository)
        {
            _mapper = mapper;
            _grupoProdutoRepository = grupoProdutoRepository;
        }
        public async Task Deletar(int id)
        {
            await _grupoProdutoRepository.Deletar(id);
        }

        public async Task<GrupoProdutoDTO> Obter(int id)
        {
            var retorno = await _grupoProdutoRepository.Obter(id);
            return _mapper.Map<GrupoProdutoDTO>(retorno);
        }

        public async Task<PagedResultDTO<GrupoProdutoDTO>> ObterPaginado(GrupoProdutoFilterDTO filters)
        {
            var retorno = await _grupoProdutoRepository.ObterPaginado(filters);
            return _mapper.Map<PagedResultDTO<GrupoProdutoDTO>>(retorno);
        }

        public async Task<GrupoProdutoDTO> Salvar(GrupoProdutoDTO grupo)
        {
            var fornecedorModel = await _grupoProdutoRepository.Salvar(_mapper.Map<GrupoProduto>(grupo));
            return _mapper.Map<GrupoProdutoDTO>(fornecedorModel);
        }
    }
}
