using Dapper;
using Dommel;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.Infrastructure.Persistence.Repositories
{
    public class PermissaoRepository : BaseRepository<Permissao>, IPermissaoRepository
    {
        public PermissaoRepository(DbSession session) : base(session)
        {
        }

        async Task<IList<Permissao>> IPermissaoRepository.ObterPermissoes()
        {
            return (IList<Permissao>)await _session.Connection.GetAllAsync<Permissao>();
        }

        async Task<IList<Permissao>> IPermissaoRepository.ObterPermissoesPorIdPerfil(int idPerfil)
        {
            var sql = @$"SELECT permissao.* FROM Perfil p 
                        INNER JOIN PermissaoPerfil pp ON p.id = pp.Perfil_Id
                        INNER JOIN Permissao permissao ON pp.permissao_id =  permissao.id
                        WHERE p.id = @idPerfil";
            var retorno = await _session.Connection.QueryAsync<Permissao>(sql, new
            {
                idPerfil
            });
            return retorno.ToList();
        }

        async Task<IList<Permissao>> IPermissaoRepository.ObterPermissoesPorIdUsuario(int idUsuario)
        {
            var sql = @$"SELECT
                            permissao.*
                            FROM permissao permissao
                            INNER JOIN categoriapermissao cp ON permissao.Categoria_id = cp.Id
                            INNER JOIN permissaoPerfil pp ON permissao.id = pp.permissao_id
                            INNER JOIN perfil perfil ON pp.perfil_id = perfil.id
                            INNER JOIN perfilUsuario pu ON perfil.id = pu.perfil_id
                            INNER JOIN usuario u ON pu.usuario_id = u.id
                            WHERE 1 = 1
                            AND permissao.ativo = 1
                            AND cp.ativo = 1
                            AND perfil.ativo =1 
                            AND u.ativo =1 
                            AND u.id = @id";
            var retorno = await _session.Connection.QueryAsync<Permissao>(sql, new
            {
                id = idUsuario
            });
            return retorno.Distinct().ToList();
        }
    }
}
