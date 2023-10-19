using JC.Core.Dtos;
using JC.Infrastructure.Shared;
using JC.Infrastructure.Shared.Attributes;

namespace JC.Application.Services
{
    [Service]
    public interface ICaixaService
    {
        Task<CaixaDTO> ObterCaixaPorDia(DateTime? data = null);
        Task<CaixaDTO> Salvar(CaixaDTO record);

        Task<CaixaDTO> AbrirCaixa(CaixaDTO record);

        Task<CaixaDTO> FecharCaixa(CaixaDTO record);
    }
}
