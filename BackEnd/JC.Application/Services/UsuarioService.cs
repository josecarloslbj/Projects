using AutoMapper;
using Dommel;
using JC.Application.Exceptions;
using JC.Core.Base;
using JC.Core.Comuns;
using JC.Core.Dtos;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Hash;
using JC.Infrastructure.Shared.Authorization.Model;
using JC.Infrastructure.Shared.JwtUtils;
using Microsoft.Extensions.Options;

namespace JC.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IMapper _mapper;

        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;
        private readonly IPermissaoService _permissaoService;

        public UsuarioService(IUsuarioRepository usuarioRepository,
            IPerfilUsuarioRepository perfilUsuarioRepository,
            IMapper mapper,
            IPerfilRepository perfilRepository,
               IJwtUtils jwtUtils,
        IOptions<AppSettings> appSettings,
        IPermissaoService permissaoService)
        {
            _usuarioRepository = usuarioRepository;
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _mapper = mapper;
            _perfilRepository = perfilRepository;

            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            _permissaoService = permissaoService;
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

        public IEnumerable<User> GetAll()
        {
            //return _context.Users;
            return null;
        }

        public User GetById(int id)
        {
            User user = new User();
            user.Id = 1;
            user.Username = "usuario TESTE";
            user.LastName = "usuario last name";
            user.LastName = "usuario fisrt name";
            //var user = _context.Users.Find(id);
            //if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var usuario = ObterUsuarioLoginSenha(model.Username, model.Password).Result;
            if (usuario == null)
                throw new Exception("Username or password is incorrect");
            // throw new AppException("Username or password is incorrect");

            User user = new User();
            user.Id = usuario.Id;
            user.Username = usuario.Nome!;
            user.PasswordHash = usuario.Senha!;

            var permissoes = new List<string>();
            var retornoPermissoes = _permissaoService.ObterPermissoesIdUsuario(usuario.Id).Result;
            if (retornoPermissoes != null)
            {
                permissoes = retornoPermissoes.Select(q => q.Nome!).Distinct().ToList();
            }
            //// validate
            //if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
            //    throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            // user.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from user
            // removeOldRefreshTokens(user);

            // save changes to db
            //_context.Update(user);
            //_context.SaveChanges();

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token, permissoes);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                //_context.Update(user);
                //_context.SaveChanges();
            }

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");
            //throw new AppException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from user
            removeOldRefreshTokens(user);


            var permissoes = new List<string>();
            var retornoPermissoes = _permissaoService.ObterPermissoesIdUsuario(user.Id).Result;
            if (retornoPermissoes != null)
            {
                permissoes = retornoPermissoes.Select(q => q.Nome!).Distinct().ToList();
            }

            // save changes to db
            //_context.Update(user);
            //_context.SaveChanges();

            // generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token, permissoes);
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");
            // throw new AppException("Invalid token");

            // revoke token and save
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            //_context.Update(user);
            //_context.SaveChanges();
        }


        private User getUserByRefreshToken(string token)
        {
            //var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            //if (user == null)
            //    throw new AppException("Invalid token");

            //return user;

            // throw new AppException("Invalid token");

            return new User
            {
                Id = 99
            };
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void removeOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}
