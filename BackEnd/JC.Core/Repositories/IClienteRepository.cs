using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<Cliente> Obter(int id);
        Task Deletar(int id);
        Task<Cliente> Salvar(Cliente fabricante);
        Task<PagedResultDTO<Cliente>> ObterPaginado(FilterDTO filters);
        Task<Cliente> ObterCompleto(int id);
    }
}
