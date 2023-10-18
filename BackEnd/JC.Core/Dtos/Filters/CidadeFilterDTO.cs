using JC.Core.Base;

namespace JC.Core.Dtos.Filters
{
    public class CidadeFilterDTO : PagedInputDTO
    {
        public string? Nome { get; set; }
        public string? Codigo { get; set; }
        public int IdEstado { get; set; }
    }
}
