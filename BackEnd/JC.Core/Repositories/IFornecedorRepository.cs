using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;

namespace JC.Core.Repositories
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> Obter(int id);
        Task Deletar(int id);
        Task<Fornecedor> Salvar(Fornecedor fornecedor);
        Task<PagedResultDTO<Fornecedor>> ObterPaginado(FornecedorFilterDTO filters);

    }
}
