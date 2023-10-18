using JC.Core.Base;

namespace JC.Core.Dtos.Filters
{
    public class PerfilFilter : PagedInputDTO
    {
        public bool IncluirInativos { get; set; }
        public string? Nome { get; set; }
    }
}
