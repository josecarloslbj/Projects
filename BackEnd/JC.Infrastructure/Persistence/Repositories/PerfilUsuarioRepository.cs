using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class PerfilUsuarioRepository : BaseRepository<PerfilUsuario>, IPerfilUsuarioRepository
    {
        public PerfilUsuarioRepository(DbSession session) : base(session)
        {
        }
    }
}
