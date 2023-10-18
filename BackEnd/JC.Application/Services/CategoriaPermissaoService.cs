using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Domain.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services
{
    [ExcludeFromCodeCoverageAttribute]
    public class CategoriaPermissaoService : ICategoriaPermissaoService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaPermissaoService(
             ICategoriaRepository categoriaRepository
            )
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IList<Categoria>> ObterCategoriasPermissao()
        {
            return await _categoriaRepository.ObterCategorias();
        }
    }
}
