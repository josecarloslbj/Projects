import { SituacaoPedido } from "src/app/shared/enumeradores/enum";
import { ProdutoVendaModel } from "../../produto/model/produto-model";

export class ItemPedidoModel {
    id: number = 0;
    idPedido!: number;
    idProduto!: number;
    descricao!: string;
    quantidade!: number;
    valorUnitario!: number;
    valorDesconto!: number;
    valorAcrescimo!: number;   
    valorTotal!: number;   
    situacaoAtual!: SituacaoPedido;
}