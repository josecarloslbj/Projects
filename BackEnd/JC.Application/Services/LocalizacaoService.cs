using AutoMapper;
using JC.Application.Exceptions;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Dtos;
using JC.Core.Repositories;

using System.Diagnostics.CodeAnalysis;
using JC.Core.Entities.Localizacao;

namespace JC.Application.Services 
{
    [ExcludeFromCodeCoverageAttribute]
    public class LocalizacaoService : ILocalizacaoService
    {
        private readonly ILocalizacaoRepository<Pais> _paisRepository;
        private readonly ILocalizacaoRepository<Estado> _estadoRepository;
        private readonly ILocalizacaoRepository<Cidade> _cidadeRepository;
        private readonly ILocalizacaoRepository<Bairro> _bairroRepository;
        private readonly IMapper _mapper;

        public LocalizacaoService(ILocalizacaoRepository<Pais> paisRepository,
            ILocalizacaoRepository<Estado> estadoRepository,
            ILocalizacaoRepository<Cidade> cidadeRepository,
            ILocalizacaoRepository<Bairro> bairroRepository,
            IMapper mapper)
        {
            _paisRepository = paisRepository;
            _estadoRepository = estadoRepository;
            _cidadeRepository = cidadeRepository;
            _bairroRepository = bairroRepository;
            _mapper = mapper;
        }

        async Task<PagedResultDTO<Pais>> ILocalizacaoService.ObterPaisPaginado(PagedInputDTO filters)
        {
            return await _paisRepository.GetListPagedAsync(filters);
        }

        async Task<PagedResultDTO<Estado>> ILocalizacaoService.ObterEstadoPaginado(EstadoFilterDTO filters)
        {
            return await _estadoRepository.ObterEstadoPaginado(filters);
        }

        async Task<PagedResultDTO<Cidade>> ILocalizacaoService.ObterCidadePaginado(CidadeFilterDTO filters)
        {
            return await _cidadeRepository.ObterCidadesPaginado(filters);
        }

        async Task<PagedResultDTO<Bairro>> ILocalizacaoService.ObterBairroPaginado(BairroFilterDTO filters)
        {
            return await _bairroRepository.ObterBairroPaginado(filters);
        }

        async Task<PaisDTO> ILocalizacaoService.SalvarPais(PaisDTO dto)
        {
            var pais = await _paisRepository.SaveOrUpdate(_mapper.Map<Pais>(dto));
            return _mapper.Map<PaisDTO>(pais);
        }

        async Task<PaisDTO> ILocalizacaoService.ObterPais(int id)
        {
            var pais = await _paisRepository.GetAsync(id);
            if (pais == null) throw new DomainLayerException("Pais não localizado.");
            return _mapper.Map<PaisDTO>(pais);
        }

        async Task<BairroDTO> ILocalizacaoService.SalvarBairro(BairroDTO dto)
        {
            var record = await _bairroRepository.SaveOrUpdate(_mapper.Map<Bairro>(dto));
            return _mapper.Map<BairroDTO>(record);
        }

        async Task<BairroDTO> ILocalizacaoService.ObterBairro(int id)
        {
            var record = await _bairroRepository.GetAsync(id);
            if (record == null) throw new DomainLayerException("Bairro não localizado.");

            var cidade = await _cidadeRepository.GetAsync(record.IdCidade);
            if (cidade != null)
            {
                var estado = await _estadoRepository.GetAsync(cidade.IdEstado);
                if (estado != null)
                    cidade.Estado = estado;

                record.Cidade = cidade;
            }

            return _mapper.Map<BairroDTO>(record);
        }
    }
}