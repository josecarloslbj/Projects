using JC.Core.Base;

namespace JC.Core.Dtos.Filters
{
    public class EstadoFilterDTO : PagedInputDTO
    {
        public string? Nome { get; set; }
        public string? Sigla { get; set; }
        public string? Codigo { get; set; }
        public int IdPais { get; set; }
    }
}
