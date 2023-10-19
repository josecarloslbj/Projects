using Dommel;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class PermissaoPerfilRepository : BaseRepository<PermissaoPerfil>, IPermissaoPerfilRepository
    {
        private DbSession _session;
        public PermissaoPerfilRepository(DbSession session) : base(session)
        {
            _session = session;
        }
        public async Task<PermissaoPerfil> Salvar(PermissaoPerfil permissaoPerfil)
        {
            var retorno = await _session.Connection.InsertAsync(permissaoPerfil);
            if (retorno != null)
            {
                permissaoPerfil.Id = Convert.ToInt32(retorno);
            }
            return permissaoPerfil;
        }

        public async Task<IList<PermissaoPerfil>> ObterPermissaoByIdPerfil(int idPerfil)
        {
            var sql = "SELECT * FROM PermissaoPerfil WHERE Perfil_Id = @idPerfil";
            var retorno = await _session.Connection.SelectAsync<PermissaoPerfil>(q => q.IdPerfil == idPerfil);
            return retorno.ToList();
        }
    }
}
