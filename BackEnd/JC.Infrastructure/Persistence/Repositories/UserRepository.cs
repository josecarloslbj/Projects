using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Domain.Repositories
{
    public class UserRepository : BaseRepository<Perfil>, IUserRepository
    {
        private DbSession _session;
        public UserRepository(DbSession session) : base(session)
        {
            _session = session;
        }
    }
}
