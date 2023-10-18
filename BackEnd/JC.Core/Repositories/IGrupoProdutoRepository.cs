using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;

namespace JC.Core.Repositories
{
    public interface IGrupoProdutoRepository
    {
        Task<GrupoProduto> Obter(int id);
        Task Deletar(int id);
        Task<GrupoProduto> Salvar(GrupoProduto grupoProduto);
        Task<PagedResultDTO<GrupoProduto>> ObterPaginado(GrupoProdutoFilterDTO filters);
    }
}
