using Dapper;
using JC.Core.Base;

namespace JC.Core.Entities
{
    [Table("Unidade")]
    public class Unidade : EntidadeBase
    {
        public string? Nome { get; set; }
        public int? TipoUnidade { get; set; }
        public string? CpfCnpj { get; set; }
        public string? Descricao { get; set; }
        public bool IsUnidadePadrao { get; set; }
        public string? Email { get; set; }
        public string? TipoEmail { get; set; }
        public string? CamposComplementares { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

        public string? CEP { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public int? IdBairro { get; set; }

    }
}
