using JC.Core.Base;

namespace JC.Core.Entities.Localizacao
{
    public class Cidade : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? Codigo { get; set; }
        public int IdEstado { get; set; }
        public bool Padrao { get; set; }
        public virtual Estado? Estado { get; set; }
        public int? IdUsuario { get; set; }
    }
}
