using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Domain.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services 
{
    [ExcludeFromCodeCoverageAttribute]
    public class PermissaoService : IPermissaoService
    {
        private readonly IPermissaoPerfilRepository _permissaoPerfilRepository;
        private readonly IPermissaoRepository _permissaoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        public PermissaoService(
             IPermissaoPerfilRepository permissaoPerfilRepository
            , ICategoriaRepository categoriaRepository
            , IPermissaoRepository permissaoRepository)
        {
            _permissaoPerfilRepository = permissaoPerfilRepository;
            _categoriaRepository = categoriaRepository;
            _permissaoRepository = permissaoRepository;
        }

        public async Task<IList<Permissao>> ObterPermissoes()
        {
            var permissoes = await _permissaoRepository.ObterPermissoes();
            var categorias = await _categoriaRepository.ObterCategorias();

            foreach (var item in permissoes)
            {
                var categoria = categorias.Where(q => q.Id == item.IdCategoria).FirstOrDefault();
                if (categoria != null)
                    item.Categoria = categoria;
            }

            return permissoes.ToList();
        }

        public async Task<IList<Permissao>> ObterPermissoesIdUsuario(int idUsuario)
        {
            var permissoes = await _permissaoRepository.ObterPermissoesPorIdUsuario(idUsuario);

            var grupo = permissoes.GroupBy(q => q.Nome).Select(w => w.FirstOrDefault()!).Distinct().ToList();

            return grupo;
        }
    }
}
