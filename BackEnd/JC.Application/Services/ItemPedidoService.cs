using JC.Core.Dtos;
using JC.Domain.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services 
{
    [ExcludeFromCodeCoverageAttribute]
    public class ItemPedidoService : IItemPedidoService
    {
        public async Task<ItemPedidoDTO> Criar(ItemPedidoDTO record)
        {
            throw new NotImplementedException();
        }
    }
}
