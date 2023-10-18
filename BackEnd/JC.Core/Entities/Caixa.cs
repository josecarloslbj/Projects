using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JC.Core.Base;
using JC.Core.Comuns;

namespace JC.Core.Entities
{
    public class Caixa : EntidadeBase
    {
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public int IdUsuarioAbertura { get; set; }
        public int? IdUsuarioFechamento { get; set; }
        public int? IdUsuarioConferenciaAbertura { get; set; }
        public int? IdUsuarioConferenciaFechamento { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SituacaoCaixa Situacao { get; set; }

        public virtual string SituacaoStr
        {
            get => Situacao.ToString();
            private set => Situacao = Enum.TryParse(value, out SituacaoCaixa enumCast) ? enumCast : default(SituacaoCaixa);

        }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }

        public string? ObservacaoAbertura { get; set; }
        public string? ObservacaoFechamento { get; set; }

        public virtual IList<HistoricoCaixa>? HistoricoCaixas { get; set; }
        public virtual void AddHistorico(HistoricoCaixa hist)
        {
            this.HistoricoCaixas?.Add(hist);
        }
    }
}
