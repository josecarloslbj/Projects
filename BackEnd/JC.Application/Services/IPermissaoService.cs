using JC.Core.Entities;

namespace JC.Application.Services
{
    public interface IPermissaoService
    {
        Task<IList<Permissao>> ObterPermissoes();

        Task<IList<Permissao>> ObterPermissoesIdUsuario(int idUsuario);
    }
}
