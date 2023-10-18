using JC.Core.Base;
using JC.Core.Entities;

namespace JC.Core.Repositories
{
    public interface IUnidadeRepository : IBaseRepository<Unidade>
    {
        Task<Unidade> Obter(int id);
        Task<Unidade> Salvar(Unidade record);
        Task<PagedResultDTO<Unidade>> ObterPaginado(PagedInputDTO filters);
    }
}
