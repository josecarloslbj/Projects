using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;
using JC.Core.Entities.Localizacao;

namespace JC.Domain.Interfaces.Services
{
    public interface ILocalizacaoService
    {
        Task<PagedResultDTO<Pais>> ObterPaisPaginado(PagedInputDTO filters);
        Task<PagedResultDTO<Estado>> ObterEstadoPaginado(EstadoFilterDTO filters);
        Task<PagedResultDTO<Cidade>> ObterCidadePaginado(CidadeFilterDTO filters);
        Task<PagedResultDTO<Bairro>> ObterBairroPaginado(BairroFilterDTO filters);

        Task<PaisDTO> ObterPais(int id);
        Task<PaisDTO> SalvarPais(PaisDTO dto);
        Task<BairroDTO> ObterBairro(int id);
        Task<BairroDTO> SalvarBairro(BairroDTO dto);
    }
}
