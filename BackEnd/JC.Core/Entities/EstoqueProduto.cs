using JC.Core.Base;

namespace JC.Core.Entities
{
    public class EstoqueProduto : EntidadeBase
    {
        public int QtdeAtual { get; set; }
        public int IdProduto { get; set; }
        public DateTime? DataReposicao { get; set; }
        public int? QtdeUltimaReposicao { get; set; }
        public int? IdUsuario { get; set; }

        public string? Observacao { get; set; }

        public List<HistoricoEstoqueProduto>? Historico { get; set; }
        public virtual void AddHistorico(HistoricoEstoqueProduto hist)
        {
            this.Historico?.Add(hist);
        }
    }
}
