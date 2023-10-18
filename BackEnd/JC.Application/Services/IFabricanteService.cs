using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;

namespace JC.Domain.Interfaces.Services
{
    public interface IFabricanteService
    {
        Task<FabricanteDTO> Obter(int id);
        Task Deletar(int id);
        Task<FabricanteDTO> Salvar(FabricanteDTO usuario);
        Task<PagedResultDTO<FabricanteDTO>> ObterPaginado(FabricanteFilterDTO filters);
    }
}
