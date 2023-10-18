using JC.Core.Base;

namespace JC.Core.Entities
{
    public class GrupoProduto : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int? IdUsuario { get; set; }
    }
}

