using JC.Core.Base;
using JC.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.Application.Services
{
    public interface IUnidadeService
    {
        Task<UnidadeDTO> Salvar(UnidadeDTO dto);

        Task<UnidadeDTO> Obter(int id);

        Task<UnidadeDTO> Deletar(int id);

        Task<PagedResultDTO<UnidadeDTO>> ObterTodos(PagedInputDTO filters);
    }
}
