using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;

namespace JC.Domain.Interfaces.Services
{
    public interface IPedidoService
    {
        Task<PedidoDTO> Obter(int id);

        Task<PedidoDTO> Criar(PedidoDTO record);

        Task<PagedResultDTO<PedidoDTO>> ObterPaginado(FilterDTO filters);
    }
}
