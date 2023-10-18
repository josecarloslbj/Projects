using JC.Core.Base;

namespace JC.Core.Entities
{
    public class HistoricoEstoqueProduto : EntidadeBase
    {
        public int IdEstoqueProduto { get; set; }
        public int QtdeAtual { get; set; }     
        public DateTime? DataReposicao { get; set; }        
        public int? IdUsuario { get; set; }
        public string? Observacao { get; set; }
    }
}
