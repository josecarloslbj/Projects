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
    public class FabricanteService : IFabricanteService
    {
        private readonly IMapper _mapper;
        private readonly IFabricanteRepository _fabricanteRepository;
        public FabricanteService(IMapper mapper,
            IFabricanteRepository fabricanteRepository)
        {
            _mapper = mapper;
            _fabricanteRepository = fabricanteRepository;
        }

        public async Task Deletar(int id)
        {
            await _fabricanteRepository.Deletar(id);
        }

        public async Task<FabricanteDTO> Obter(int id)
        {
            var retorno = await _fabricanteRepository.Obter(id);
            return _mapper.Map<FabricanteDTO>(retorno);
        }

        public async Task<PagedResultDTO<FabricanteDTO>> ObterPaginado(FabricanteFilterDTO filters)
        {
            var retorno = await _fabricanteRepository.ObterPaginado(filters);
            return _mapper.Map<PagedResultDTO<FabricanteDTO>>(retorno);
        }

        public async Task<FabricanteDTO> Salvar(FabricanteDTO fabricante)
        {
            var fornecedorModel = await _fabricanteRepository.Salvar(_mapper.Map<Fabricante>(fabricante));
            return _mapper.Map<FabricanteDTO>(fornecedorModel);
        }
    }
}
