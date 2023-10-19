import { SituacaoCaixa } from "src/app/shared/enumeradores/enum";

export class AberturaFechamentoCaixaModel {
    id: number = 0;
    valorInicial!: number;
    valorFinal!: number;
    idUsuarioAbertura!: number;
    nomeUsuarioAbertura!: string;
    nomeUsuarioFechamento!: string;
    idUsuarioFechamento!: number;
    idUsuarioConferenciaAbertura!: number;
    idUsuarioConferenciaFechamento!: number;
    observacaoAbertura!: string;
    observacaoFechamento!: string;
    situacaoStr!: string;
    situacao!: SituacaoCaixa;
    dataAbertura!: Date;
    dataFechamento!: Date;

}