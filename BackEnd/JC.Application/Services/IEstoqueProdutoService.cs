using JC.Infrastructure.Shared;
using JC.Core.Dtos;
using JC.Infrastructure.Shared.Attributes;

namespace JC.Application.Services
{
    [Service]
    public interface IEstoqueProdutoService
    {
        Task<EstoqueProdutoDTO?> RealizarBaixaEstoque(ItemPedidoDTO itemPedido);

        Task<EstoqueProdutoDTO> Salvar(EstoqueProdutoDTO record);

    }
}
