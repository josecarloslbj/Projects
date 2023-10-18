using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<Produto> Obter(int id);
        Task Deletar(int id);
        Task<Produto> Salvar(Produto fabricante);
        Task<PagedResultDTO<Produto>> ObterPaginado(ProdutoFilterDTO filters);
        Task<Produto> ObterCompleto(int id);
    }
}
