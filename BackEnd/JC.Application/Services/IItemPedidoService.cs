using JC.Core.Dtos;

namespace JC.Application.Services
{
    public interface IItemPedidoService
    {
        Task<ItemPedidoDTO> Criar(ItemPedidoDTO record);
    }
}
