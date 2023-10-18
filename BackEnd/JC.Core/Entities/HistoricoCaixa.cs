using JC.Core.Base;
using JC.Core.Comuns;

namespace JC.Core.Entities
{
    public class HistoricoCaixa : EntidadeBase
    {
        public decimal Valor { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataAtual { get; set; }
        public string? Motivo { get; set; }
        public TipoMovimentacaoCaixa TipoMovimento { get; set; }
    }
}
