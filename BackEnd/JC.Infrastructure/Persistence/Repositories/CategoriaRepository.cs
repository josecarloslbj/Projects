using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared;

namespace JC.Infrastructure.Persistence.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(DbSession session) : base(session)
        {
        }

        Task<IList<Categoria>> ICategoriaRepository.ObterCategorias()
        {
            throw new NotImplementedException();
        }

        Categoria ICategoriaRepository.Salvar(Categoria categoria)
        {
            throw new NotImplementedException();
        }
    }
}
