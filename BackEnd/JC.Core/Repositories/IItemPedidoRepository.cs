using JC.Core.Entities;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface IItemPedidoRepository : IBaseRepository<ItemPedido>
    {
        Task<ItemPedido> Criar(ItemPedido record);
    }
}
