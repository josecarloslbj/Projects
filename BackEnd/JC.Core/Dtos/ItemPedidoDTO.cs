using JC.Core.Comuns;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JC.Core.Dtos
{
    public class ItemPedidoDTO
    {
        public int? Id { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProduto { get; set; }
        public string? Descricao { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorAcrescimo { get; set; }
        public decimal? ValorTotal { get; set; }
        public virtual ProdutoDTO? Produto { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SituacaoPedido SituacaoAtual { get; set; }

        public virtual string SituacaoAtualStr
        {
            get => SituacaoAtual.ToString();
            private set => SituacaoAtual = Enum.TryParse(value, out SituacaoPedido enumCast) ? enumCast : default(SituacaoPedido);

        }
    }
}
