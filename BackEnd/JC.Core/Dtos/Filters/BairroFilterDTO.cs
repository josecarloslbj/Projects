using JC.Core.Base;

namespace JC.Core.Dtos.Filters
{
    public class BairroFilterDTO : PagedInputDTO
    {
        public string? Nome { get; set; }
        public string? Codigo { get; set; }
        public int IdCidade { get; set; }
    }
}
