using JC.Core.Entities;

namespace JC.Application.Services
{
    public interface ICategoriaPermissaoService
    {
        Task<IList<Categoria>> ObterCategoriasPermissao();
    }
}
