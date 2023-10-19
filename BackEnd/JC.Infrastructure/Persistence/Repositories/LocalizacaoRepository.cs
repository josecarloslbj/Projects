using Dapper;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities.Localizacao;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;
using static Dapper.SqlMapper;

namespace JC.Repository.Repositories
{
    public class LocalizacaoRepository<T> : BaseRepository<T>, ILocalizacaoRepository<T> where T : EntidadeBase
    {
        private DbSession _session;
        public LocalizacaoRepository(DbSession session) : base(session)
        {
            _session = session;
        }

        public Task<PagedResultDTO<Pais>> ObterPaisPaginado(PagedInputDTO filters)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDTO<Estado>> ObterEstadoPaginado(EstadoFilterDTO filters)
        {
            PagedResultDTO<Estado> pagedResult = new PagedResultDTO<Estado>();

            string sql = $@"SELECT 
                                    e.id,e.Nome,e.CodigoUf,e.padrao, p.id, p.Nome, p.Descricao,p.padrao
                            FROM Estado e
                            INNER JOIN Pais p ON  e.Pais_Id = p.id ";

            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();

            if (filters.IdPais > 0)
            {
                filters.Filtros.Add(new ParametrosFilters
                {
                    Condicao = $" AND e.Pais_id = @idPais"
                });
            }

            sql += BaseRepository<Estado>.ObterFiltros(filters);

            var total = _session.Connection.RecordCount<Estado>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var estadoDictionary = new Dictionary<int, Estado>();
            var paisDictionary = new Dictionary<int, Pais>();

            var retorno = await _session.Connection.QueryAsync<Estado, Pais, Estado>(
                sql
                , (e, p) =>
                     {
                         if (!estadoDictionary.TryGetValue(e.Id, out var estado))
                         {
                             estado = e;
                         }

                         if (p != null)
                         {
                             estado.Pais = p;
                         }

                         return e;
                     },
                     param: new
                     {
                         idPais = filters.IdPais
                     },
                     transaction: this._session.Transaction,
                     splitOn: "Id");

            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Estado>();
            return pagedResult;
        }

        public async Task<PagedResultDTO<Cidade>> ObterCidadesPaginado(CidadeFilterDTO filters)
        {
            PagedResultDTO<Cidade> pagedResult = new PagedResultDTO<Cidade>();

            string sql = $@"SELECT c.*,e.* 
                                FROM  Cidade c
                                INNER JOIN Estado e ON c.estado_id = e.id ";

            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();

            if (filters.IdEstado > 0)
            {
                filters.Filtros.Add(new ParametrosFilters
                {
                    Condicao = $" AND c.Estado_id = @idEstado"
                });
            }

            sql += BaseRepository<Estado>.ObterFiltros(filters);

            var total = _session.Connection.RecordCount<Estado>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var cidadeDictionary = new Dictionary<int, Cidade>();
            var estadoDictionary = new Dictionary<int, Estado>();

            var retorno = await _session.Connection.QueryAsync<Cidade, Estado, Cidade>(
                sql
                , (c, e) =>
                {
                    if (!cidadeDictionary.TryGetValue(c.Id, out var cidade))
                    {
                        cidade = c;
                    }

                    if (e != null)
                    {
                        cidade.Estado = e;
                    }

                    return c;
                },
                     param: new
                     {
                         idEstado = filters.IdEstado
                     },
                     transaction: this._session.Transaction,
                     splitOn: "Id");

            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Cidade>();
            return pagedResult;
        }

        public async Task<PagedResultDTO<Bairro>> ObterBairroPaginado(BairroFilterDTO filters)
        {
            PagedResultDTO<Bairro> pagedResult = new PagedResultDTO<Bairro>();

            
            string sql = $@"SELECT b.*,c.* 
                                FROM Bairro b
                                INNER JOIN Cidade c ON b.cidade_id = c.id ";

            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();

            if (filters.IdCidade > 0)
            {
                filters.Filtros.Add(new ParametrosFilters
                {
                    Condicao = $" AND c.id = @idCidade"
                });
            }

            sql += BaseRepository<Bairro>.ObterFiltros(filters);

            var total = _session.Connection.RecordCount<Estado>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var bairroDictionary = new Dictionary<int, Bairro>();
            var estadoDictionary = new Dictionary<int, Estado>();

            var retorno = await _session.Connection.QueryAsync<Bairro, Cidade, Bairro>(
                sql
                , (b, c) =>
                {
                    if (!bairroDictionary.TryGetValue(b.Id, out var bairro))
                    {
                        bairro = b;
                    }

                    if (c != null)
                    {
                        bairro.Cidade = c;
                    }

                    return b;
                },
                     param: new
                     {
                         idCidade = filters.IdCidade
                     },
                     transaction: this._session.Transaction,
                     splitOn: "Id");

            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Bairro>();
            return pagedResult;
        }

    }
}
