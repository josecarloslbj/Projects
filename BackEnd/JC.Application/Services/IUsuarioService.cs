using JC.Core.Base;
using JC.Core.Dtos;

namespace JC.Domain.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> Salvar(UsuarioDTO usuario);

        Task<UsuarioDTO> ObterUsuario(int id);

        Task<UsuarioDTO> Deletar(int id);

        Task<PagedResultDTO<UsuarioDTO>> ObterTodos(PagedInputDTO filters);


        Task<UsuarioDTO> ObterUsuarioLoginSenha(string login, string senha);
    }
}
