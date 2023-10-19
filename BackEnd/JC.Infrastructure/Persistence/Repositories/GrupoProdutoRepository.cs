using Dapper;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class GrupoProdutoRepository : BaseRepository<GrupoProduto>, IGrupoProdutoRepository
    {
        private DbSession _session;
        public GrupoProdutoRepository(DbSession session) : base(session)
        {
            _session = session;
        }

        public async Task Deletar(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<GrupoProduto> Obter(int id)
        {
            return await GetAsync(id);
        }

        public async Task<PagedResultDTO<GrupoProduto>> ObterPaginado(GrupoProdutoFilterDTO filters)
        {
            PagedResultDTO<GrupoProduto> pagedResult = new PagedResultDTO<GrupoProduto>();

            string sql = $@"SELECT f.*
                                FROM  GrupoProduto f
                                 ";

            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();

            sql += BaseRepository<GrupoProduto>.ObterFiltros(filters);

            var total = _session.Connection.RecordCount<GrupoProduto>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<GrupoProduto>(sql, param: new { });

            pagedResult.Items = retorno != null ? retorno.ToList() : new List<GrupoProduto>();
            return pagedResult;
        }

        public async Task<GrupoProduto> Salvar(GrupoProduto grupoProduto)
        {
            return await SaveOrUpdate(grupoProduto);
        }
    }
}
