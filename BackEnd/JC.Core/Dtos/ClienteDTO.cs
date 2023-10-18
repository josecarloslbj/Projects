using JC.Core.Entities;
using Newtonsoft.Json;

namespace JC.Core.Dtos
{
    public class ClienteDTO
    {
        public int? Id { get; set; }
        public int TipoPessoa { get; set; }

        public string? Nome { get; set; }

        [JsonProperty("cpj_cnpj")]
        public string? CPF_CNPJ { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }

        public string? Observacao { get; set; }
        public string? Senha { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? IdArquivo { get; set; }

        public int? IdUsuario { get; set; }
        public virtual Arquivo? Arquivo { get; set; }

        public bool Ativo { get; set; }
    }
}
