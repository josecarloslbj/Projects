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
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;
        private readonly IPermissaoPerfilRepository _permissaoPerfilRepository;
        private readonly IPermissaoRepository _permissaoRepository;
        public readonly IMapper _mapper;
        public PerfilService(
            IPerfilRepository perfilRepository,
            IPermissaoPerfilRepository permissaoPerfilRepository,
            IPermissaoRepository permissaoRepository,
            IMapper mapper)
        {
            _perfilRepository = perfilRepository;
            _mapper = mapper;
            _permissaoPerfilRepository = permissaoPerfilRepository;
            _permissaoRepository = permissaoRepository;
        }

        public async Task<Perfil> ObterPorId(int id)
        {
            var perfil = await _perfilRepository.ObterPorId(id);

            if (perfil == null)
                throw new Exception("Perfil não localizado");

            var permissoes = await _permissaoRepository.ObterPermissoesPorIdPerfil(id);
            if (permissoes != null)
                perfil.Permissoes = permissoes.ToList();

            return perfil;
        }

        public async Task<PagedResultDTO<Perfil>> ObterTodos(PagedInputDTO filters)
        {
            try
            {
                var perfis = await _perfilRepository.ObterPaginado(filters);
                return perfis;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Perfil> Salvar(PerfilDTO perfil)
        {
            if (perfil == null)
                throw new Exception("Perfil NULO");


            var _perfil = _mapper.Map<Perfil>(perfil);

            if (_perfil.IsEdicao)
            {
                var permissoesAntigas = await _permissaoPerfilRepository.ObterPermissaoByIdPerfil(_perfil.Id);
                foreach (var item in permissoesAntigas)
                    await _permissaoPerfilRepository.DeleteAsync(item.Id);
            }

            var retornoPerfil = await _perfilRepository.Salvar(_perfil);
            if (retornoPerfil == null)
                throw new Exception("Não foi possível Salvar PERFIL");

            int idPerfil = _perfil.Id;

            if (retornoPerfil.Permissoes != null)
            {
                foreach (var permissao in retornoPerfil.Permissoes)
                {
                    PermissaoPerfil pp = new PermissaoPerfil();
                    pp.IdPerfil = idPerfil;
                    pp.IdPermissao = permissao.Id;

                    await _permissaoPerfilRepository.Salvar(pp);
                }
            }


            return _perfil;
        }

        public async Task<Perfil> DeletarPerfil(int id)
        {
            var perfil = await this.ObterPorId(id);
            var permissoesAntigas = await _permissaoPerfilRepository.ObterPermissaoByIdPerfil(id);
            foreach (var item in permissoesAntigas)
                await _permissaoPerfilRepository.DeleteAsync(item.Id);

            await _perfilRepository.Delete(id);

            return perfil;
        }

        public async Task<IList<PerfilDTO>> ObterPerfilByFilter(PerfilFilter? filter)
        {
            var perfis = await _perfilRepository.ObterPerfilByFilter(filter);
            var perfilDTO = _mapper.Map<List<PerfilDTO>>(perfis);
            return perfilDTO;
        }
    }
}
