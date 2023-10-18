using JC.Core.Comuns;
using JC.Core.Entities;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace JC.Core.Dtos
{
    public class PedidoDTO
    {
        public int? Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descricao { get; set; }
        public decimal? ValorPedido { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorAcrescimo { get; set; }
        public decimal? ValorTotal { get; set; }

        public DateTime DataCadastro { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdCliente { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FormaPagamento FormaPagamento { get; set; }

        public virtual string FormaPagamentoStr
        {
            get => FormaPagamento.ToString();
            set => FormaPagamento = Enum.TryParse(value, out FormaPagamento enumCast) ? enumCast : default(FormaPagamento);
        }


        [JsonConverter(typeof(StringEnumConverter))]
        public SituacaoPedido SituacaoAtual { get; set; }

        public virtual string SituacaoAtualStr
        {
            get => SituacaoAtual.ToString();
            private set => SituacaoAtual = Enum.TryParse(value, out SituacaoPedido enumCast) ? enumCast : default(SituacaoPedido);
        }


        public List<ItemPedidoDTO>? ItensPedido { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
