using Dapper;
using JC.Core.Base;

namespace JC.Core.Entities
{
    [Table("Permissao")]
    public class Permissao : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Ajuda { get; set; }
        public int IdCategoria { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
