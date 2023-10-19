using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;

namespace JC.Application.Services
{
    public interface IClienteService
    {
        Task<ClienteDTO> Salvar(ClienteDTO usuario);

        Task<ClienteDTO> Obter(int id);

        Task<ClienteDTO> Deletar(int id);

        Task<PagedResultDTO<ClienteDTO>> ObterPaginado(FilterDTO filters);
    }
}
