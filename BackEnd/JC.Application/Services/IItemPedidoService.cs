using JC.Core.Dtos;

namespace JC.Domain.Interfaces.Services
{
    public interface IItemPedidoService
    {
        Task<ItemPedidoDTO> Criar(ItemPedidoDTO record);
    }
}
