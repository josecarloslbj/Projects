using Dapper;
using Dommel;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;
using JC.Infrastructure.Shared.Uow;
using Newtonsoft.Json.Linq;

namespace JC.Repository.Repositories
{
    public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        private DbSession _session;
        public PerfilRepository(DbSession session) : base(session)
        {
            _session = session;
        }

        public async Task<Perfil> ObterPorId(int id)
        {
            return await _session.Connection.GetAsync<Perfil>(id, _session.Transaction);
        }

        public async Task<Perfil> Salvar(Perfil perfil)
        {
            if (perfil.IsEdicao)
                _session.Connection.Update(perfil, _session.Transaction);
            else
            {
                var retorno = await _session.Connection.InsertAsync(perfil, _session.Transaction);
                if (retorno != null)
                {
                    perfil.Id = Convert.ToInt32(retorno);
                }
            }
            return perfil;
        }

        public async Task<PagedResultDTO<Perfil>> ObterPaginado(PagedInputDTO filters)
        {
            var pagedResult = new PagedResultDTO<Perfil>();
            var sql = @$"SELECT * FROM Perfil ";
            sql += ObterFiltros(filters);

            var total = await _session.Connection.RecordCountAsync<Perfil>();
            pagedResult.TotalCount = Convert.ToInt16(total);

            var retorno = await _session.Connection.QueryAsync<Perfil>(sql);
            pagedResult.Items = retorno != null ? retorno.ToList() : new List<Perfil>();

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

        public async Task<IList<Perfil>> ObterPerfilByFilter(PerfilFilter? filter)
        {
            var sql = @$"SELECT * FROM Perfil WHERE 1=1";

            if (filter == null) filter = new PerfilFilter();

            if (!filter.IncluirInativos)
            {
                sql += $" AND ativo = true";
            }

            if (!string.IsNullOrEmpty(filter.Nome))
                sql += $" AND ativo = @nome";

            sql += " ORDER BY Nome";

            var retorno = await _session.Connection.QueryAsync<Perfil>(sql, new
            {
                ativo = filter.IncluirInativos,
                nome = filter.Nome,
            });

            return retorno != null ? retorno.ToList() : new List<Perfil>();
        }

    }
}
