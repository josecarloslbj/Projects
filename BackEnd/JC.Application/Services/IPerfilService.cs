using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;

namespace JC.Domain.Interfaces.Services
{
    public interface IPerfilService
    {
        Task<Perfil> ObterPorId(int id);
        Task<Perfil> Salvar(PerfilDTO perfil);
        Task<PagedResultDTO<Perfil>> ObterTodos(PagedInputDTO filters);

        Task<Perfil> DeletarPerfil(int id);

        Task<IList<PerfilDTO>> ObterPerfilByFilter(PerfilFilter? filter);
    }
}
