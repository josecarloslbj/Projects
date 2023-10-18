using Dapper;

using JC.Core.Base;

namespace JC.Core.Entities
{
    [Table("Perfil")]
    public class Perfil : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<Permissao>? Permissoes { get; set; }

        public int? IdUsuario { get; set; }
    }
}
