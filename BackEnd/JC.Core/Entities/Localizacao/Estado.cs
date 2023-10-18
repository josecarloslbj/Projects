using JC.Core.Base;

namespace JC.Core.Entities.Localizacao
{
    public class Estado : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? Sigla { get; set; }
        public string? Codigo { get; set; }
        public int IdPais { get; set; }
        public bool Padrao { get; set; }

        public virtual Pais? Pais { get; set; }
        public int? IdUsuario { get; set; }
    }
}
