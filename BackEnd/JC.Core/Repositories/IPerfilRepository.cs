using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface IPerfilRepository : IBaseRepository<Perfil>
    {
        Task<Perfil> ObterPorId(int id);
        Task<Perfil> Salvar(Perfil perfil);
        Task<PagedResultDTO<Perfil>> ObterPaginado(PagedInputDTO filters);
        Task<IList<Perfil>> ObterPerfilByFilter(PerfilFilter? filter);
    }
}
