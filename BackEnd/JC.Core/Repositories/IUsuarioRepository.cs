
using JC.Core.Entities;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> Obter(int idUsuario);
        Task<Usuario> Salvar(Usuario usuario);
        Task<PagedResultDTO<Usuario>> ObterPaginado(PagedInputDTO filters);
    }
}
