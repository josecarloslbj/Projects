using JC.Core.Base;

namespace JC.Core.Entities
{
    public class Endereco : EntidadeBase
    {
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public int? Bairro_Id { get; set; }
        public IList<ContatoPessoa>? Contato { get; set; }
    }
}
