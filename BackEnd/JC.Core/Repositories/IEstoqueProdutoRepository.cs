using JC.Core.Base;
using JC.Core.Entities;

namespace JC.Core.Repositories
{
    public interface IEstoqueProdutoRepository : IBaseRepository<EstoqueProduto>
    {
        Task<EstoqueProduto> Salvar(EstoqueProduto record);
    }
}
