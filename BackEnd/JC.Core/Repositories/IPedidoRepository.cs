using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        Task<Pedido> Obter(int id);

        Task<Pedido> Criar(Pedido record);

        Task<PagedResultDTO<Pedido>> ObterPaginado(FilterDTO filters);
    }
}
