using Dommel;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared;

namespace JC.Infrastructure.Persistence.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly IPermissaoRepository _permissaoRepository;
        public CategoriaRepository(DbSession session
            , IPermissaoRepository permissaoRepository) : base(session)
        {
            _permissaoRepository = permissaoRepository;
        }

        async Task<IList<Categoria>> ICategoriaRepository.ObterCategorias()
        {
            var categorias = await _session.Connection.GetAllAsync<Categoria>();
            var permissoes = await _permissaoRepository.ObterPermissoes();

            foreach (var item in categorias)
                item.Permissoes = permissoes.ToList().Where(q => q.IdCategoria == item.Id).ToList();

            return categorias.ToList();
        }

        Categoria ICategoriaRepository.Salvar(Categoria categoria)
        {
            if (categoria.IsEdicao)
                _session.Connection.Update(categoria);
            else
            {
                var retorno = _session.Connection.Insert(categoria);
            }
            return categoria;
        }
    }
}
