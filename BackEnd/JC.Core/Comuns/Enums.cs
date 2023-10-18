using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace JC.Core.Comuns;

[JsonConverter(typeof(StringEnumConverter))]
public enum SistemaOperacional
{
    ANDROID,
    IOS,
    WINDOWS,
    NAO_IDENTIFICADO
}
public enum Idioma
{
    [Description("Indefinido")] Indefinido = 0,

    [Description("Português")] pt_BR = 1,

    [Description("Espanhol")] es_MX = 2,

    [Description("Inglês")] en_US = 3
}

public enum FormaPagamento
{
    INDEFINIDO = 0,
    BOLETO_BANCARIO = 1,
    CARTAO_DEBITO = 2,
    CARTAO_CREDITO = 3,
    DINHEIRO = 4,
    CREDIARIO = 5,
    PIX = 7,
    TRANSFERENCIA_BANCARIA = 7,
}

public enum SituacaoPedido
{
    INDEFINIDO = 0,
    CRIADO = 1,
    AGUARDANDO = 2,
    CANCELADO = 3,
    AGUARDANDO_ASSINATURA = 4,
    PAGO = 5,
    PAGO_PARCIAL = 6,
    EM_CANCELAMENTO = 7,
    ESTORNADO = 8
}

public enum SituacaoCaixa
{
    [Description("Indefinido")]
    INDEFINIDO = 0,
    [Description("Aguardando Abertura")]
    AGUARDANDO_ABERTURA = 1,
    [Description("Aberto")]
    ABERTO = 2,
    [Description("Fechado")]
    FECHADO = 3,
}

public enum TipoMovimentacaoCaixa
{
    [Description("Indefinido")]
    INDEFINIDO = 0,
    [Description("Entrada")]
    ENTRADA = 1,
    [Description("Saída")]
    SAIDA = 2,
}
