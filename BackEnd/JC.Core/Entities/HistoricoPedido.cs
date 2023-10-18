using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using JC.Core.Base;
using JC.Core.Comuns;

namespace JC.Core.Entities
{
    public class HistoricoPedido : EntidadeBase
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SituacaoPedido Situacao { get; set; }

        public virtual string SituacaoStr
        {
            get => Situacao.ToString();
            set => Situacao = Enum.TryParse(value, out SituacaoPedido enumCast) ? enumCast : default(SituacaoPedido);
        }

        public string? Descricao { get; set; }

    }
}
