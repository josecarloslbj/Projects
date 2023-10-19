using Dapper;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;


namespace JC.Repository.Repositories
{
    public class FabricanteRepository : BaseRepository<Fabricante>, IFabricanteRepository
    {
        private DbSession _session;
        public FabricanteRepository(DbSession session) : base(session)
        {
            _session = session;
        }

        public async Task Deletar(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<Fabricante> Obter(int id)
        {
            return await GetAsync(id);
        }

        public async Task<PagedResultDTO<Fabricante>> ObterPaginado(FabricanteFilterDTO filters)
        {
            PagedResultDTO<Fabricante> pagedResult = new PagedResultDTO<Fabricante>();

            string sql = $@"SELECT f.*
                                FROM  Fabricante f
                                 ";

            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();

            sql += BaseRepository<Fornecedor>.ObterFiltros(filters);

            var total = _session.Connection.RecordCount<Fabricante>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<Fabricante>(sql, param: new { });

            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Fabricante>();
            return pagedResult;
        }

        public async Task<Fabricante> Salvar(Fabricante fabricante)
        {
            return await SaveOrUpdate(fabricante);
        }
    }
}
