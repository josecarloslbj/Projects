import { FormaPagamento, SituacaoPedido } from "src/app/shared/enumeradores/enum";
import { PagedInputDTO } from "src/app/shared/paginator/pagedInputDTO";
import { UsuarioModel } from "../../usuarios/model/usuarios-model";
import { ItemPedidoModel } from "./item-pedido.model";

export class PedidoModel extends PagedInputDTO {
    id: number = 0;
    codigo!: string;
    descricao!: string;
    dataCadastro!: Date;
    valorPedido!: number;
    formaPagamento: FormaPagamento | undefined;
    formaPagamentoStr!: string;
    valorDesconto: number = 0;
    valorAcrescimo: number = 0;
    valorTotal: number = 0;
    idUsuario!: number;
    idCliente!: number | null;
    itensPedido: ItemPedidoModel[] = [];
    situacaoAtual!: SituacaoPedido;
    usuario!:UsuarioModel;
}

export class PedidoFilterModel extends PagedInputDTO {
    dataInicio?: Date;
    dataFim?: Date;
}