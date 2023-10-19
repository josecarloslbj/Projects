using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class HistoricoEstoqueProdutoRepository : BaseRepository<HistoricoEstoqueProduto>, IHistoricoEstoqueProdutoRepository
    {
        public HistoricoEstoqueProdutoRepository(DbSession session) : base(session)
        {
        }
    }
}
