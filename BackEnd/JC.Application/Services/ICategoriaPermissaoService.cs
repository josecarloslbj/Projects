using JC.Core.Entities;

namespace JC.Domain.Interfaces.Services
{
    public interface ICategoriaPermissaoService
    {
        Task<IList<Categoria>> ObterCategoriasPermissao();
    }
}
