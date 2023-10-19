import { FormaPagamento } from "src/app/shared/enumeradores/enum";
import { PagedInputDTO } from "src/app/shared/paginator/pagedInputDTO";

export class ProdutoModel extends PagedInputDTO {
    id: number = 0;
    nome: string | undefined;
    descricao!: string | undefined;
    codigo!: string | undefined;
    qtde: number = 1;
    qtdeMin: number = 3;
    preco!: number;
    valorCusto!: number;
    idGrupo!: number | null;
    idFabricante!: number | null;
    idFornecedor!: number | null;
    unidadeMedida!: string | undefined;
    localizacao!: string | undefined;
    idArquivo: number | undefined = 0;
    urlArquivo: string | undefined;
    diretorio: string | undefined;
    arquivo!: ArquivoModel;
    total: number | undefined = 0;
    ativo!:boolean;
    grupo!: GrupoProdutoModel;
}
export class ProdutoVendaModel {
    id: number = 0;
    ordem!: number;
    nome: string | undefined;
    descricao!: string | undefined;
    qtde: number = 1;
    qtdeOld: number = 1;
    preco!: number;
    desconto: number = 0;
    acrescimo!: number;
    unidadeMedida!: string | undefined;
    idArquivo: number | undefined = 0;
    urlArquivo: string | undefined;
    arquivo!: ArquivoModel;
    total: number | undefined = 0;
    removido: boolean = false;
}


export class ArquivoModel {
    id: number = 0;
    nome: string | undefined;
    diretorio!: string | undefined;
    subDiretorio!: string | undefined;
    extensao!: string | undefined;
    entidade!: string | undefined;
    idEntidade!: string | undefined;
    urlArquivo: string | undefined;
}

export class GrupoProdutoModel extends PagedInputDTO {
    id: number = 0;
    nome: string | undefined;
    descricao: string | undefined;
}

export class CupomFiscalModel {
    data!: Date;
    caixa!: string;
    nomeCliente!: string;
    cpfcnpjCliente!: string;
    valorDesconto!: number
    valorAcrescimo!: number
    valorRecebido!: number
    subTotalVenda!: number
    valorTroco!: number
    observacao!: string;
    formaPagto!: string
    guid!: string
    idPedido!: number
    produtos: ProdutoVendaModel[] = [];
}
