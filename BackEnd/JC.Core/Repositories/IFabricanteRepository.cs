using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;

namespace JC.Core.Repositories
{
    public interface IFabricanteRepository
    {
        Task<Fabricante> Obter(int id);
        Task Deletar(int id);
        Task<Fabricante> Salvar(Fabricante fabricante);
        Task<PagedResultDTO<Fabricante>> ObterPaginado(FabricanteFilterDTO filters);
    }
}
