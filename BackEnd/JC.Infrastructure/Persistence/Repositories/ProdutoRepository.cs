using Dapper;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private DbSession _session;
        public ProdutoRepository(DbSession session) : base(session)
        {
            _session = session;
        }

        public async Task Deletar(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<Produto> Obter(int id)
        {
            return await ObterCompleto(id);
        }

        public async Task<PagedResultDTO<Produto>> ObterPaginado(ProdutoFilterDTO filters)
        {
            PagedResultDTO<Produto> pagedResult = new PagedResultDTO<Produto>();

            string sql = $@"SELECT f.*
                                FROM  Produto f
                                 ";

            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();

            sql += BaseRepository<Produto>.ObterFiltros(filters);

            var total = _session.Connection.RecordCount<Produto>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<Produto>(sql, param: new { });

            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Produto>();
            return pagedResult;
        }

        public async Task<Produto> Salvar(Produto produto)
        {
            return await SaveOrUpdate(produto);
        }

        public async Task<Produto> ObterCompleto(int id)
        {
            string sql = $@"SELECT p.*,a.* FROM Produto p 
                            LEFT JOIN Arquivo a ON p.Arquivo_id = a.id 
                            WHERE p.id = @idProduto";


            var produtoDictionary = new Dictionary<int, Produto>();
            var arquivoDictionary = new Dictionary<int, Arquivo>();

            var retorno = await _session.Connection.QueryAsync<Produto, Arquivo, Produto>(
               sql
               , (p, a) =>
               {
                   if (!produtoDictionary.TryGetValue(p.Id, out var produto))
                   {
                       produto = p;
                   }

                   if (a != null)
                   {
                       produto.Arquivo = a;
                   }

                   return p;
               },
               param: new
               {
                   idProduto = id
               }, transaction: this._session.Transaction, splitOn: "Id");

            return retorno == null ? new Produto() : retorno?.FirstOrDefault();
        }
    }
}
