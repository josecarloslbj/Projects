using JC.Core.Entities;
using JC.Core.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services;

[ExcludeFromCodeCoverageAttribute]
public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(
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
