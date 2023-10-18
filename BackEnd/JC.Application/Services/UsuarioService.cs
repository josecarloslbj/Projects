using AutoMapper;
using Dommel;
using JC.Application.Exceptions;
using JC.Core.Base;
using JC.Core.Comuns;
using JC.Core.Dtos;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Domain.Services;
using JC.Infrastructure.Hash;

namespace JC.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository,
            IPerfilUsuarioRepository perfilUsuarioRepository,
            IMapper mapper,
            IPerfilRepository perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _mapper = mapper;
            _perfilRepository = perfilRepository;
        }

        public async Task<UsuarioDTO> Salvar(UsuarioDTO usuario)
        {
            if (!usuario.IsValid())
                throw new DomainLayerException("Informe os dados corretamente.");

            if (usuario.Perfis == null || !usuario.Perfis.Any())
                throw new DomainLayerException("Informe ao menos um perfil.");

            DateTime dataCriacao = DateTime.Now;

            if (usuario.Id == 0)
            {
                usuario.DataCadastro = dataCriacao;

                string senha = usuario.Senha;
                usuario.Senha = Hash.HashSHA256(senha, dataCriacao.ToString(Constantes.FORMAT_SALT));
                usuario.UltimaSenhaGerada = Hash.EncryptMD5(senha);
            }
            else
            {
                var perfilUsuarioAntigo = await _perfilUsuarioRepository.SelectAsync(q => q.IdUsuario == usuario.Id);

                foreach (var item in perfilUsuarioAntigo)
                    await _perfilUsuarioRepository.DeleteAsync(item.Id);

                var _user = await _usuarioRepository.GetAsync(usuario.Id);
                if (_user != null) usuario.Senha = _user.Senha;
            }

            var _usuario = _mapper.Map<Usuario>(usuario);

            var retorno = await _usuarioRepository.SaveOrUpdate(_usuario);
            if (retorno != null) usuario.Id = retorno.Id;

            if (usuario.Perfis != null)
            {
                foreach (var item in usuario.Perfis)
                {
                    var perfilUsuario = new PerfilUsuario();
                    perfilUsuario.IdUsuario = usuario.Id;
                    perfilUsuario.IdPerfil = item.Id;

                    await _perfilUsuarioRepository.SaveOrUpdate(_mapper.Map<PerfilUsuario>(perfilUsuario));
                }
            }

            return usuario;
        }

        public async Task<UsuarioDTO> ObterUsuario(int id)
        {
            var usuario = await _usuarioRepository.Get(id);

            if (usuario == null) throw new DomainLayerException($"Usuário não localizado para o id:{id}.");

            var perfilUsuario = await _perfilUsuarioRepository.SelectAsync(q => q.IdUsuario == id);

            List<int> idsPerfis = perfilUsuario.Select(q => q.IdPerfil).ToList();

            var perfis = await _perfilRepository.SelectAsync(q => idsPerfis.Contains(q.Id));
            if (perfis != null && perfis.Any())
                usuario.Perfis = perfis.ToList();

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> Deletar(int id)
        {
            var usuario = await ObterUsuario(id);

            if (usuario == null) throw new DomainLayerException($"Usuário não localizado para o id:{id}.");

            await _usuarioRepository.DeleteAsync(id);

            return usuario;
        }

        public async Task<PagedResultDTO<UsuarioDTO>> ObterTodos(PagedInputDTO filters)
        {
            var retorno = await _usuarioRepository.ObterPaginado(filters);

            return _mapper.Map<PagedResultDTO<UsuarioDTO>>(retorno);
        }

        public async Task<UsuarioDTO> ObterUsuarioLoginSenha(string login, string senha)
        {
            UsuarioDTO? usuario = null;

            var usuariosByLogin = await _usuarioRepository.SelectAsync(q => q.Login == login);
            if (usuariosByLogin != null)
            {
                if (usuariosByLogin.Count() > 1)
                    throw new DomainLayerException("Verificar login existente.");

                usuario = _mapper.Map<UsuarioDTO>(usuariosByLogin.FirstOrDefault());
                if (usuario != null)
                {
                    DateTime dataCriacao = usuario.DataCadastro.Value;

                    var hashSenha2 = Hash.HashSHA256(senha, dataCriacao.ToString(Constantes.FORMAT_SALT));
                    if (string.Equals(usuario.Senha, hashSenha2))
                    {
                        return usuario;
                    }
                    else
                        usuario = null;
                }
            }

            return usuario;
        }
    }
}
