using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;

namespace JC.Application.Services
{
    public interface IGrupoProdutoService
    {
        Task<GrupoProdutoDTO> Obter(int id);
        Task Deletar(int id);
        Task<GrupoProdutoDTO> Salvar(GrupoProdutoDTO grupo);
        Task<PagedResultDTO<GrupoProdutoDTO>> ObterPaginado(GrupoProdutoFilterDTO filters);

    }
}
