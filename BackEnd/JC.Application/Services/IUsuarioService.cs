using JC.Core.Base;
using JC.Core.Dtos;
using JC.Infrastructure.Shared.Authorization.Model;

namespace JC.Application.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> Salvar(UsuarioDTO usuario);

        Task<UsuarioDTO> ObterUsuario(int id);

        Task<UsuarioDTO> Deletar(int id);

        Task<PagedResultDTO<UsuarioDTO>> ObterTodos(PagedInputDTO filters);

        Task<UsuarioDTO> ObterUsuarioLoginSenha(string login, string senha);


        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress);
        Task<AuthenticateResponse> RefreshToken(string token, string refreshToken, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
