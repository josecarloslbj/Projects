using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;

namespace JC.Domain.Interfaces.Services
{
    public interface IFornecedorService
    {
        Task<FornecedorDTO> Obter(int id);

        Task Deletar(int id);
        Task<FornecedorDTO> Salvar(FornecedorDTO usuario);

        Task<PagedResultDTO<FornecedorDTO>> ObterPaginado(FornecedorFilterDTO filters);
    }
}
