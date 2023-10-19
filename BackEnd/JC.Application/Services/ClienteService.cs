using AutoMapper;
using JC.Application.Exceptions;
using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;

using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services
{
    [ExcludeFromCodeCoverageAttribute]
    public class ClienteService : IClienteService
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;


        public ClienteService(IMapper mapper,
            IClienteRepository clienteRepository,
            IPerfilUsuarioRepository perfilUsuarioRepository
              )
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _perfilUsuarioRepository = perfilUsuarioRepository;
        }


        public async Task<ClienteDTO> Deletar(int id)
        {
            var cliente = _clienteRepository.Get(id);
            if (cliente == null) throw new DomainLayerException($"Cliente {id} não encontrado.");

            await _clienteRepository.Deletar(id);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<PagedResultDTO<ClienteDTO>> ObterPaginado(FilterDTO filters)
        {
            var retorno = await _clienteRepository.ObterPaginado(filters);

            return _mapper.Map<PagedResultDTO<ClienteDTO>>(retorno);
        }


        public async Task<ClienteDTO> Obter(int id)
        {
            var usuario = await _clienteRepository.ObterCompleto(id);

            if (usuario == null) throw new DomainLayerException($"Cliente {id} não encontrado.");

            var perfilUsuario = await _perfilUsuarioRepository.SelectAsync(q => q.IdUsuario == id);

            List<int> idsPerfis = perfilUsuario.Select(q => q.IdPerfil).ToList();

            //var perfis = await _perfilRepository.SelectAsync(q => idsPerfis.Contains(q.Id));
            //if (perfis != null && perfis.Any())
            //    usuario.Perfis = perfis.ToList();

            return _mapper.Map<ClienteDTO>(usuario);
        }

        public async Task<ClienteDTO> Salvar(ClienteDTO record)
        {
            record.IdUsuario = Contexto.Atual?.IdUsuario;

            if (record.Id == 0)
                record.DataCadastro = DateTime.Now;
            else
            {
                record.DataAlteracao = DateTime.Now;


                //var perfilUsuarioAntigo = await _perfilUsuarioRepository.SelectAsync(q => q.IdUsuario == usuario.Id);

                //foreach (var item in perfilUsuarioAntigo)
                //    await _perfilUsuarioRepository.DeleteAsync(item.Id);
            }

            //if (usuario.Id == 0)
            //{
            //    string senha = usuario.Senha;
            //    usuario.Senha = Hash.HashSHA256(usuario.Senha, usuario.DataCadastro.Value.ToString(JC.Comuns.Constantes.FORMAT_SALT));
            //    usuario.UltimaSenhaGerada = Hash.EncryptMD5(senha);
            //}

            var retorno = await _clienteRepository.SaveOrUpdate(_mapper.Map<Cliente>(record));
            record.Id = retorno.Id;

            //if (usuario.Perfis != null)
            //{
            //    foreach (var item in usuario.Perfis)
            //    {
            //        var perfilUsuario = new PerfilUsuario();
            //        perfilUsuario.IdUsuario = usuario.Id;
            //        perfilUsuario.IdPerfil = item.Id;

            //        await _perfilUsuarioRepository.SaveOrUpdate(_mapper.Map<PerfilUsuario>(perfilUsuario));
            //    }
            //}

            return record;
        }
    }
}
