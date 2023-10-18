using JC.Core.Entities;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface IPermissaoPerfilRepository : IBaseRepository<PermissaoPerfil>
    {
        Task<PermissaoPerfil> Salvar(PermissaoPerfil permissaoPerfil);

        Task<IList<PermissaoPerfil>> ObterPermissaoByIdPerfil(int idPerfil);
    }
}
