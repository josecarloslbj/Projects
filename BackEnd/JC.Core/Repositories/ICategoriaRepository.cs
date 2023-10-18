using JC.Core.Base;
using JC.Core.Entities;

namespace JC.Core.Repositories;

public interface ICategoriaRepository : IBaseRepository<Categoria>
{
    Categoria Salvar(Categoria categoria);
    Task<IList<Categoria>> ObterCategorias();
}
