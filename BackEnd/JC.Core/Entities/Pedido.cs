using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using JC.Core.Base;
using JC.Core.Comuns;

namespace JC.Core.Entities
{
    public class Pedido : EntidadeBase
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FormaPagamento FormaPagamento { get; set; }

        public virtual string FormaPagamentoStr
        {
            get => FormaPagamento.ToString();
            set => FormaPagamento = Enum.TryParse(value, out FormaPagamento enumCast) ? enumCast : default(FormaPagamento);
        }

        public decimal ValorPedido { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorAcrescimo { get; set; }
        public decimal ValorTotal { get; set; }
        public int IdUsuario { get; set; }
        public int? IdCliente { get; set; }
        public int? IdHistoricoAtual { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SituacaoPedido SituacaoAtual { get; set; }

        public virtual string SituacaoAtualStr
        {
            get => SituacaoAtual.ToString();
            private set => SituacaoAtual = Enum.TryParse(value, out SituacaoPedido enumCast) ? enumCast : default(SituacaoPedido);

        }

        public virtual IList<ItemPedido>? ItensPedido { get; set; }

        public virtual IList<HistoricoPedido>? Historico { get; set; }


        public virtual Usuario Usuario { get; set; }

        internal void AdicionarHistorico(SituacaoPedido situacaoAtual)
        {
            AdicionarHistorico(situacaoAtual, string.Empty);
        }

        internal void AdicionarHistorico(SituacaoPedido situacaoAtualPedido, string descricao)
        {
            if (this.Historico == null)
                this.Historico = new List<HistoricoPedido>();

            var historicoPedido = new HistoricoPedido();

            this.Historico.Add(historicoPedido);
        }
    }
}
