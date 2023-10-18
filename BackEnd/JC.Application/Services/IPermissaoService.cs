using JC.Core.Entities;

namespace JC.Domain.Interfaces.Services
{
    public interface IPermissaoService
    {
        Task<IList<Permissao>> ObterPermissoes();

        Task<IList<Permissao>> ObterPermissoesIdUsuario(int idUsuario);
    }
}
