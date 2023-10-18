using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;

namespace JC.Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        Task<ProdutoDTO> Obter(int id);

        Task Deletar(int id);
        Task<ProdutoDTO> Salvar(ProdutoDTO produto);

        Task<PagedResultDTO<ProdutoDTO>> ObterPaginado(ProdutoFilterDTO filters);

        Task<ProdutoDTO> ObterPorCodigoBarras(string codigoBarras);
    }
}
