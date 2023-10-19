using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        private DbSession _session;
        public ItemPedidoRepository(DbSession session) : base(session)
        {
            _session = session;
        }

        public async Task<ItemPedido> Criar(ItemPedido record)
        {
            return await SaveOrUpdate(record);
        }
    }
}
