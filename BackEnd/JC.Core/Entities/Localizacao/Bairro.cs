using JC.Core.Base;

namespace JC.Core.Entities.Localizacao
{
    public class Bairro : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? ZonaGeografica { get; set; }
        public string? RegionalAdministrativa { get; set; }
        public int Populacao { get; set; }
        public decimal AreaKm2 { get; set; }
        public int IdCidade { get; set; }
        public int? IdUsuario { get; set; }
        public bool Padrao { get; set; }

        public virtual Cidade? Cidade { get; set; }
    }
}
