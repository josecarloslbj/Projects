using JC.Core.Base;
using JC.Core.Entities;

namespace JC.Core.Repositories;

public interface IPermissaoRepository : IBaseRepository<Permissao>
{
    Task<IList<Permissao>> ObterPermissoes();

    Task<IList<Permissao>> ObterPermissoesPorIdPerfil(int idPerfil);

    Task<IList<Permissao>> ObterPermissoesPorIdUsuario(int idUsuario);
}
