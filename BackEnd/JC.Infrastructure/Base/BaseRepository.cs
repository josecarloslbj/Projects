using Dommel;
using JC.Core.Base;
using JC.Infrastructure.Shared.Uow;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace JC.Infrastructure.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntidadeBase
    {
        private readonly DbSession _connectionProvider;
        protected string ConnectionString { get; set; }

        public DbSession _session;


        private readonly IDbConnection Connection;

        public IDbTransaction Transaction { get { return _session.Transaction; } }
        public BaseRepository(DbSession session)
        {
            _session = session;

            _connectionProvider = session;
            Connection = session.Connection;


            Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.MySQL);
        }

        public async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var retorno = await _session.Connection.SelectAsync(predicate);

            return retorno;
        }

        public async Task<PagedResultDTO<TEntity>> GetListPagedAsync(PagedInputDTO filters)
        {
            var pagedResult = new PagedResultDTO<TEntity>();
            var sql = @$"SELECT * FROM {typeof(TEntity).Name} ";
            sql += ObterFiltros(filters);

            var total = _session.Connection.Count<TEntity>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<TEntity>(sql);
            pagedResult.Items = retorno != null ? retorno.ToList() : new List<TEntity>();

            return pagedResult;
        }

        public async Task<PagedResultDTO<TEntity>> GetListPagedAsync(string sql, PagedInputDTO filters)
        {
            var pagedResult = new PagedResultDTO<TEntity>();
            sql += ObterFiltros(filters);

            var total = _session.Connection.Count<TEntity>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<TEntity>(sql);
            pagedResult.Items = retorno != null ? retorno.ToList() : new List<TEntity>();

            return pagedResult;
        }

        public static string ObterFiltros(PagedInputDTO filters)
        {
            if (filters == null) return string.Empty;


            var sql = " WHERE 1=1 ";

            if (filters != null && filters.Filters != null)
            {
                var filtess = filters.Filters.ToString();
                try
                {
                    JObject o = JObject.Parse(filtess);

                    List<JProperty> listaitems = new List<JProperty>();


                    List<string> listaColunas = new List<string>();
                    string filterGlobal = string.Empty;

                    foreach (var e in o)
                    {
                        if (e.Value.Children().Count() == 2)
                        {

                            var coluna = e.Key.ToString();
                            var tipo = ((JProperty)e.Value.Children().Last()).Value.ToString().ToLower();
                            var f = ((JProperty)e.Value.Children().First()).Value.ToString();


                            listaColunas.Add(coluna);


                            if (coluna.Equals("global")) filterGlobal = f;


                            if (!string.IsNullOrWhiteSpace(f) && !coluna.Equals("global"))
                            {
                                string condicao = "";
                                if (tipo == "contém" || tipo == "contains")
                                {
                                    condicao = $"AND {coluna} like '%{f}%'";
                                }
                                else if (tipo == "igual" || tipo == "equals")
                                {
                                    condicao = $"AND {coluna} = '{f}'";
                                }
                                else if (tipo == "começa com" || tipo == "startswith")
                                {
                                    condicao = $"AND {coluna}  like '{f}%'";
                                }
                                else if (tipo == "termina com" || tipo == "endswith")
                                {
                                    condicao = $"AND {coluna} like '%{f}'";
                                }
                                sql += condicao;
                            }
                        }
                        else
                        {

                        }
                    }
                    bool existeFilterGlobal = listaColunas.Where(q => q.Equals("global")).Any();

                    if (existeFilterGlobal)
                    {
                        sql = " WHERE 1=1 ";

                        string condicao = " AND ( ";

                        int count = 0;
                        foreach (var coluna in listaColunas.Where(q => !q.Equals("global")))
                        {
                            if (count == 0)
                                condicao += $" {coluna} like '%{filterGlobal}%' ";
                            else
                                condicao += $" OR {coluna} like '%{filterGlobal}%'";

                            count++;
                        }

                        condicao += " ) ";

                        sql += condicao;
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }

            if (filters.Filtros != null)
            {
                foreach (var item in filters.Filtros)
                {
                    sql += item.Condicao;
                }
            }

            if (!string.IsNullOrEmpty(filters.SortField))
            {
                sql += $" ORDER BY {filters.SortField}";

                sql += filters.SortOrder == 1 ? " ASC " : " DESC ";
            }

            if (filters != null && filters.Filters != null)
            {
                sql += $" LIMIT {filters.PageSize} OFFSET {filters.First}";
            }
            return sql;
        }



        public async Task<TEntity> SaveOrUpdate(TEntity entity)
        {
            if (entity.IsEdicao)
            {
                await Update(entity);
            }
            else
            {
                var result = await Add(entity);
                entity.Id = Convert.ToInt32(result);
            }

            return entity;
        }

        public async Task<int?> Add(TEntity entity)
        {
            var result = await _session.Connection.InsertAsync(entity, transaction: Transaction);
            return Convert.ToInt32(result);
        }

        public async Task Delete(int id)
        {
            //await using var connection = new
            //(ConnectionString);
            //var entity = connection.Get<TEntity>(id);
            //await connection.DeleteAsync(entity);
            //await connection.CloseAsync();

            var entity = await _session.Connection.GetAsync<TEntity>(id);
            await _session.Connection.DeleteAsync(entity, transaction: Transaction);
        }

        public async Task<int> Update(TEntity entity)
        {
            var result = await _session.Connection.UpdateAsync(entity, transaction: Transaction);
            return Convert.ToInt32(result);
            //await using var connection = new MySqlConnection(ConnectionString);
            //var result = connection.Update(entity);
            //await connection.CloseAsync();
            //return Convert.ToInt32(result);
        }

        public async Task<TEntity> Get(int id)
        {
            var result = await _session.Connection.GetAsync<TEntity>(id);
            return result;
        }

        public async Task<TEntity> Get(long id)
        {
            return await _session.Connection.GetAsync<TEntity>(id);

            //await using var connection = new MySqlConnection(ConnectionString);
            //var result = await connection.GetAsync<TEntity>(id);
            //await connection.CloseAsync();
            //return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _session.Connection.GetAllAsync<TEntity>();

        }

        public async Task<IEnumerable<TEntity>> GetListPaged(int pageNumber, int rowPerPages, string conditions, string orderby)
        {
            //await using var connection = new MySqlConnection(ConnectionString);
            //var result = await connection.GetListPagedAsync<TEntity>(pageNumber, rowPerPages, conditions, orderby);
            //await connection.CloseAsync();
            //return result;
            throw new Exception("TODO");
        }

        public async Task<long> Count(string conditions)
        {
            //await using var connection = new MySqlConnection(ConnectionString);
            //var result = await connection.RecordCountAsync<long>(conditions);
            //await connection.CloseAsync();
            //return result;
            throw new Exception("TODO");
        }

        public virtual async Task<int?> AddAsync(TEntity entity)
        {
            return await AddAsync(entity, CancellationToken.None).ConfigureAwait(false);
        }

        public virtual async Task<int?> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Add(entity), cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await DeleteAsync(id, CancellationToken.None).ConfigureAwait(false);
        }

        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await Task.Run(() => Delete(id), cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => Update(entity), CancellationToken.None).ConfigureAwait(false);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Update(entity), cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await GetAsync(id, CancellationToken.None).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Get(id), cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetAllAsync(CancellationToken.None).ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(GetAll, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity> ExecuteSingle(string query, object? parameters = null)
        {
            //await using var connection = new MySqlConnection(ConnectionString);
            var result = _session.Connection.QuerySingle<TEntity>(query, parameters, commandType: CommandType.Text);
            //await connection.CloseAsync();
            return result;
            //throw new Exception("TODO");
        }

        public async Task<TEntity> ExecuteSingleOrDefault(string query, object? parameters = null)
        {
            //await using var connection = new MySqlConnection(ConnectionString);
            var result = _session.Connection.QuerySingleOrDefault<TEntity>(query, parameters, commandType: CommandType.Text);
            //await connection.CloseAsync();
            return result;
        }

        public async Task<IEnumerable<TEntity>> Execute(string query, object? parameters = null, CommandType commandType = CommandType.Text, int commandTimeOut = 60)
        {
            //await using var connection = new MySqlConnection(ConnectionString);
            //var result = connection.Query<TEntity>(query, parameters, commandType: commandType, commandTimeout: commandTimeOut);
            //await connection.CloseAsync();
            //return result;
            throw new Exception("TODO");
        }

        public async Task<T> ExecuteSingle<T>(string query, object? parameters = null, CommandType commandType = CommandType.Text, int commandTimeOut = 60)
        {
            //await using var connection = new MySqlConnection(ConnectionString);
            //var result = connection.QuerySingle<T>(query, parameters, commandType: commandType, commandTimeout: commandTimeOut);
            //await connection.CloseAsync();
            //return result;
            throw new Exception("TODO");
        }
    }
}
