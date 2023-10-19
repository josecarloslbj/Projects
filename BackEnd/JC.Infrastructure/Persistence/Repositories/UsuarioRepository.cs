
using Dapper;
using Dommel;
using JC.Core.Base;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;
using Newtonsoft.Json.Linq;

namespace JC.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DbSession session) : base(session)
        {
        }

        public async Task<Usuario> Salvar(Usuario usuario)
        {
            return await SaveOrUpdate(usuario);

        }

        public async Task<Usuario> Obter(int idUsuario)
        {
            return await Get(idUsuario);
        }


        public async Task<PagedResultDTO<Usuario>> ObterPaginado(PagedInputDTO filters)
        {
            var pagedResult = new PagedResultDTO<Usuario>();
            var sql = @$"SELECT * FROM Usuario ";
            sql += ObterFiltros(filters);

            var total = await _session.Connection.RecordCountAsync<Usuario>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<Usuario>(sql);
            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Usuario>();

            return pagedResult;
        }

        public string ObterFiltros(PagedInputDTO filters)
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

                    foreach (var e in o)
                    {
                        if (e.Value.Children().Count() == 2)
                        {

                            var colunda = e.Key.ToString();
                            var tipo = ((Newtonsoft.Json.Linq.JProperty)e.Value.Children().Last()).Value.ToString();
                            var f = ((Newtonsoft.Json.Linq.JProperty)e.Value.Children().First()).Value.ToString();
                            if (!string.IsNullOrWhiteSpace(f))
                            {
                                string condicao = "";
                                if (tipo == "Contém")
                                {
                                    condicao = $"AND {colunda} like '%{f}%'";
                                }
                                else if (tipo == "Igual")
                                {
                                    condicao = $"AND {colunda} = '{f}'";
                                }
                                else if (tipo == "Começa com")
                                {
                                    condicao = $"AND {colunda}  like '{f}%'";
                                }
                                else if (tipo == "Termina com")
                                {
                                    condicao = $"AND {colunda} like '%{f}'";
                                }

                                sql += condicao;
                            }
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }


            if (!string.IsNullOrEmpty(filters.SortField))
                sql += $" ORDER BY {filters.SortField}";

            sql += filters.SortOrder == 1 ? " ASC " : " DESC ";

            sql += $" LIMIT {filters.PageSize} OFFSET {filters.First}";
            return sql;
        }
    }
}
