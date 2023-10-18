using JC.Core.Base;
using JC.Core.Entities;

namespace JC.Core.Repositories
{
    public interface ICaixaRepository : IBaseRepository<Caixa>
    {
        Task<Caixa> Salvar(Caixa record);

        Task<Caixa> ObterCaixaDia(DateTime data);

        Task<Caixa> Obter(int id);
    }
}
