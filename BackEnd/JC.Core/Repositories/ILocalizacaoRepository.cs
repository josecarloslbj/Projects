using JC.Core.Dtos.Filters;
using JC.Core.Entities.Localizacao;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface ILocalizacaoRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntidadeBase
    {
        Task<PagedResultDTO<Pais>> ObterPaisPaginado(PagedInputDTO filters);

        Task<PagedResultDTO<Estado>> ObterEstadoPaginado(EstadoFilterDTO filters);

        Task<PagedResultDTO<Cidade>> ObterCidadesPaginado(CidadeFilterDTO filters);

        Task<PagedResultDTO<Bairro>> ObterBairroPaginado(BairroFilterDTO filters);
    }
}
