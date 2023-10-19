using Dapper;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
    {
        private DbSession _session;
        public FornecedorRepository(DbSession session) : base(session)
        {
            _session = session;
        }

        public async Task Deletar(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<Fornecedor> Obter(int id)
        {
            return await GetAsync(id);
        }

        public async Task<PagedResultDTO<Fornecedor>> ObterPaginado(FornecedorFilterDTO filters)
        {
            PagedResultDTO<Fornecedor> pagedResult = new PagedResultDTO<Fornecedor>();

            string sql = $@"SELECT f.*
                                FROM  Fornecedor f
                                 ";

            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();

            sql += BaseRepository<Fornecedor>.ObterFiltros(filters);

            var total = _session.Connection.RecordCount<Fornecedor>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<Fornecedor>(sql, param: new { });

            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Fornecedor>();
            return pagedResult;
        }

        public async Task<Fornecedor> Salvar(Fornecedor fornecedor)
        {
            return await SaveOrUpdate(fornecedor);
        }
    }
}
