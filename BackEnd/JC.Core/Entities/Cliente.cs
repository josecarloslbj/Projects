using System.Text.Json.Serialization;

using JC.Core.Base;

namespace JC.Core.Entities
{
    public class Cliente : EntidadeBase
    {
        public string? Nome { get; set; }

        [JsonPropertyName("cpj_cnpj")]
        public string? CPF_CNPJ { get; set; }
        public int TipoPessoa { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public int IdUsuario { get; set; }
        public int? IdArquivo { get; set; }
        public virtual Arquivo? Arquivo { get; set; }
    }
}
