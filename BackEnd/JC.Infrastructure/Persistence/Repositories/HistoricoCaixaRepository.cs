using JC.Infrastructure.Shared.Uow;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;

namespace JC.Repository.Repositories
{
    public class HistoricoCaixaRepository : BaseRepository<HistoricoCaixa>, IHistoricoCaixaRepository
    {
        public HistoricoCaixaRepository(DbSession session) : base(session)
        {
        }
    }
}
