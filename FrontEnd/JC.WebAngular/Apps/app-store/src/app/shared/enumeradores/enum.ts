export enum FormaPagamento {
    INDEFINIDO = <any>"INDEFINIDO", 
    BOLETO_BANCARIO = <any>"BOLETO_BANCARIO", 
    CARTAO_DEBITO = <any>"CARTAO_DEBITO", 
    CARTAO_CREDITO = <any>"CARTAO_CREDITO", 
    DINHEIRO = <any>"DINHEIRO", 
    CREDIARIO = <any>"CREDIARIO", 
    PIX = <any>"PIX", 
    TRANSFERENCIA_BANCARIA = <any>"TRANSFERENCIA_BANCARIA", 
}

export enum SituacaoPedido {
    INDEFINIDO = <any>"INDEFINIDO", 
    CRIADO = <any>"CRIADO", 
    AGUARDANDO = <any>"AGUARDANDO", 
    PAGO = <any>"PAGO", 
    PAGO_PARCIAL = <any>"PAGO_PARCIAL", 
    EM_CANCELAMENTO = <any>"EM_CANCELAMENTO", 
    CANCELADO = <any>"CANCELADO", 
    ESTORNADO = <any>"ESTORNADO", 
    ESTORNADO_PARCIAL = <any>"ESTORNADO_PARCIAL", 
}

export enum SituacaoCaixa {
    INDEFINIDO = <any>"INDEFINIDO", 
    AGUARDANDO_ABERTURA = <any>"AGUARDANDO_ABERTURA", 
    ABERTO = <any>"ABERTO", 
    FECHADO = <any>"FECHADO",
}