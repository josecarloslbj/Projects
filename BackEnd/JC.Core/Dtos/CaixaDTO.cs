using JC.Core.Comuns;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace JC.Core.Dtos
{
    public class CaixaDTO
    {
        public int Id { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public int IdUsuarioAbertura { get; set; }
        public int IdUsuarioFechamento { get; set; }
        public int IdUsuarioConferenciaAbertura { get; set; }
        public int IdUsuarioConferenciaFechamento { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SituacaoCaixa Situacao { get; set; }

        public virtual string SituacaoStr
        {
            get => Enumeradores.GetDescription(Situacao);
            set => Situacao = Enum.TryParse(value, out SituacaoCaixa enumCast) ? enumCast : default(SituacaoCaixa);

        }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }
        public string? ObservacaoAbertura { get; set; }
        public string? ObservacaoFechamento { get; set; }
        public string? NomeUsuarioAbertura { get; set; }

        public string? NomeUsuarioFechamento { get; set; }

        public string? NomeUsuarioConferenciaAbertura { get; set; }

        public string? NomeUsuarioConferenciaFechamento { get; set; }
    }



}
