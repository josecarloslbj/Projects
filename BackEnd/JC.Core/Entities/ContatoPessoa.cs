
using JC.Core.Base;

namespace JC.Core.Entities
{
    public class ContatoPessoa : EntidadeBase
    {
        public int Endereco_Id { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; }
        public string? Celular { get; }
        public string? Setor { get; }
    }
}
