using JC.Core.Base;

namespace JC.Core.Dtos.Filters
{
    public class FilterDTO : PagedInputDTO
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public bool? Ativo { get; set; }
    }
}
