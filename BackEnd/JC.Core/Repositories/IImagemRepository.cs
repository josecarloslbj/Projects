using JC.Core.Entities;
using JC.Core.Base;

namespace JC.Core.Repositories
{
    public interface IArquivoRepository : IBaseRepository<Arquivo>
    {
        Task<Arquivo> ObterPorId(int id);
        Task<Arquivo> Salvar(Arquivo arquivo, bool? removerArquivoSemUso = false);
        Task<List<Arquivo>> Deletar(List<int> ids);
        Task<List<Arquivo>> DeletarArquivosSemUso(int idArquivo);
    }
}
