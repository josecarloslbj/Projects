using JC.Core.Entities;

namespace JC.Application.Services;

public interface ICategoriaService
{
    Task<IList<Categoria>> ObterCategoriasPermissao();
}
