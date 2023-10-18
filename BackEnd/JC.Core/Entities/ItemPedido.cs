using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using JC.Core.Base;
using JC.Core.Comuns;

namespace JC.Core.Entities
{
    public class ItemPedido : EntidadeBase
    {
        public int IdPedido { get; set; }
        public int? IdProduto { get; set; }
        public string? Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorAcrescimo { get; set; }
        public decimal ValorTotal { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SituacaoPedido SituacaoAtual { get; set; }

        public virtual string SituacaoAtualStr
        {
            get => SituacaoAtual.ToString();
            private set => SituacaoAtual = Enum.TryParse(value, out SituacaoPedido enumCast) ? enumCast : default(SituacaoPedido);

        }

        public virtual Produto? Produto { get; set; }
    }
}
