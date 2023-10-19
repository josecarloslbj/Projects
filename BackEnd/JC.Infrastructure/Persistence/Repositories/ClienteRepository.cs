using Dapper;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DbSession session) : base(session)
        {

        }

        public async Task<Cliente> Obter(int id)
        {
            return await ObterCompleto(id);
        }
        public async Task Deletar(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<Cliente> Salvar(Cliente record)
        {
            return await SaveOrUpdate(record);
        }
        public async Task<PagedResultDTO<Cliente>> ObterPaginado(FilterDTO filters)
        {
            PagedResultDTO<Cliente> pagedResult = new PagedResultDTO<Cliente>();

            string sql = $@"SELECT f.*
                                FROM  Cliente f ";



            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();


            if (filters.Ativo.HasValue)
            {
                filters.Filtros.Add(new ParametrosFilters
                {
                    Condicao = filters.Ativo == true ? $" AND f.ativo = 1 " : " AND f.ativo = 0 "
                });
            }



            sql += BaseRepository<Cliente>.ObterFiltros(filters);


            var total = _session.Connection.RecordCount<Cliente>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<Cliente>(sql, param: new { });

            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Cliente>();
            return pagedResult;
        }

        public async Task<Cliente> ObterCompleto(int id)
        {
            string sql = $@"SELECT p.*,a.* FROM Cliente p 
                            LEFT JOIN Arquivo a ON p.Arquivo_id = a.id 
                            WHERE p.id = @id";


            var produtoDictionary = new Dictionary<int, Cliente>();
            var arquivoDictionary = new Dictionary<int, Arquivo>();

            var retorno = await _session.Connection.QueryAsync<Cliente, Arquivo, Cliente>(
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
                   id = id
               }, transaction: this._session.Transaction, splitOn: "Id");

            return retorno == null ? new Cliente() : retorno?.FirstOrDefault();

        }
    }
}
