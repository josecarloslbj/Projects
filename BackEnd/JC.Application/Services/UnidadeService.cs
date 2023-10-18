using AutoMapper;
using JC.Application.Exceptions;
using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Entities;
using JC.Core.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services
{
    [ExcludeFromCodeCoverageAttribute]
    public class UnidadeService : IUnidadeService
    {
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IMapper _mapper;
        public UnidadeService(IMapper mapper, IUnidadeRepository unidadeRepository)
        {
            _mapper = mapper;
            _unidadeRepository = unidadeRepository;
        }

        public async Task<UnidadeDTO> Deletar(int id)
        {
            var record = await Obter(id);

            if (record == null) throw new DomainLayerException($"Unidade não localizado para o id:{id}.");

            await _unidadeRepository.DeleteAsync(id);

            return record;
        }

        public async Task<UnidadeDTO> Obter(int id)
        {
            var record = await _unidadeRepository.Obter(id);

            if (record == null) throw new DomainLayerException($"Unidade não localizado para o id:{id}.");

            return _mapper.Map<UnidadeDTO>(record);
        }

        public async Task<PagedResultDTO<UnidadeDTO>> ObterTodos(PagedInputDTO filters)
        {
            var retorno = await _unidadeRepository.ObterPaginado(filters);

            return _mapper.Map<PagedResultDTO<UnidadeDTO>>(retorno);
        }

        public async Task<UnidadeDTO> Salvar(UnidadeDTO dto)
        {
            var record = _mapper.Map<Unidade>(dto);
            if (!record.IsEdicao)
                record.DataCriacao = DateTime.Now;

            record.DataUltimaAlteracao = DateTime.Now;

            var retorno = await _unidadeRepository.SaveOrUpdate(record);
            if (retorno != null) dto.Id = retorno.Id;

            return dto;
        }
    }
}
