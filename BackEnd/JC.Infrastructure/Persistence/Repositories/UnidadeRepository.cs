
using Dapper;
using Dommel;
using JC.Core.Base;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;
using JC.Infrastructure.Shared.Uow;
using Newtonsoft.Json.Linq;

namespace JC.Repository.Repositories
{
    public class UnidadeRepository : BaseRepository<Unidade>, IUnidadeRepository
    {       
        public UnidadeRepository(DbSession session) : base(session)
        {
            
        }

        public async Task<Unidade> Obter(int id)
        {
            return await Get(id);
        }

        public async Task<PagedResultDTO<Unidade>> ObterPaginado(PagedInputDTO filters)
        {
            var pagedResult = new PagedResultDTO<Unidade>();
            var sql = @$"SELECT * FROM Unidade ";
            sql += ObterFiltros(filters);

            var total = await _session.Connection.RecordCountAsync<Unidade>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<Unidade>(sql);
            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Unidade>();

            return pagedResult;
        }

        public async Task<Unidade> Salvar(Unidade record)
        {
            return await SaveOrUpdate(record);
        }
    }
}
