
import { Router } from '@angular/router';
import { Component, OnInit, AfterViewInit, HostListener, ViewEncapsulation, Injectable, ViewChild, ElementRef } from '@angular/core';
import {
    trigger,
    state,
    style,
    animate,
    transition,
    // ...
} from '@angular/animations';
import { FilterMetadata, MessageService, SelectItem } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { AppComponent } from 'src/app/app.component';
import { AuthenticationService } from 'src/app/login/authentication.service';
import { ProdutoService } from '../../produto/produto.service';
import { CupomFiscalModel, ProdutoModel, ProdutoVendaModel } from '../../produto/model/produto-model';
import { Subscription } from 'rxjs';
import { ClienteModel } from '../../cliente/model/cliente.model';
import { VendaService } from '../venda.service';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ProdutoComponent } from '../../produto/listar/produto.component';
import { environment } from 'src/environments/environment';
import { PedidoModel } from '../model/pedido.model';
import { ItemPedidoModel } from '../model/item-pedido.model';
import { FormaPagamento } from 'src/app/shared/enumeradores/enum';
import { ClienteService } from '../../cliente/cliente.service';
import { PagedInputDTO } from 'src/app/shared/paginator/pagedInputDTO';
import { ClienteComponent } from '../../cliente/listar/cliente.component';

declare function beep(): any;
declare function beepDelete(): any;



@Injectable({
    providedIn: 'root'
})
@Component({
    selector: 'app-venda-caixa',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],        // end of trigger block    
    templateUrl: './caixa.component.html',
    styleUrls: ['./caixa.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [DialogService, DynamicDialogRef, DynamicDialogConfig]
    // host: {
    //     '(document:keypress)': 'handleKeyboardEvent($event)'
    //   }    

})
export class VendaCaixaComponent implements OnInit, AfterViewInit {

    loading: boolean = false;

    nome!: string | undefined;
    login!: string | undefined;

    codigoBarras!: String | undefined | null;
    products: ProdutoVendaModel[] = [];
    produtosFinalizaVenda: ProdutoVendaModel[] = [];
    statuses: SelectItem[] = [];

    cupomFiscalModel!: CupomFiscalModel;

    product: ProdutoVendaModel = new ProdutoVendaModel();
    baseImg = environment.production ? environment.apiUrl : environment.imgUrl;


    time = new Date();
    rxTime = new Date();
    intervalId: any;
    subscription!: Subscription;


    @ViewChild('vlrRecebido')
    vlrRecebidoElement: any = ElementRef;
    @ViewChild('inputCodigo')
    codigoBarraElement: any = ElementRef;


    pedido = new PedidoModel();
    displayModalFinalizarCompra: boolean = false;
    displayModalCancelarCompra: boolean = false;

    displayImprimirCupom: boolean = false;
    cliente: ClienteModel = new ClienteModel();
    clientes: ClienteModel[] = [];
    selectedFormaPagto: string = FormaPagamento.DINHEIRO.toString();
    valorTotal!: number;
    valorRecebido: number | undefined;
    valorDesconto: number | undefined;
    valorAcrescimo: number = 0;
    valorTroco: number = 0;
    observacao: string | undefined;
    guid!: string;
    ref: DynamicDialogRef | undefined | null;

    executandoVenda: boolean = false;
    constructor(
        private router: Router,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private authenticationService: AuthenticationService,
        private produtoService: ProdutoService,
        private vendaService: VendaService,
        private dialogService: DialogService,
        private clienteService: ClienteService
    ) { }

    ngOnInit() {
        this.nome = this.authenticationService.userValue.username;
        // Using Basic Interval
        this.intervalId = setInterval(() => {
            this.time = new Date();
        }, 1000);
    }

    ngAfterViewInit() {
    }

    ngOnDestroy() {
        clearInterval(this.intervalId);
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }

    onRowEditInit(product: any) {
        //this.clonedProducts[product.id] = {...product};
    }

    onRowEditSave(product: any) {
        product.total = product.qtde * product.preco - product.desconto | 0;
        product.qtdeOld = product.qtde;
        //this.appComponent.showMessage('Quantidade alterada com sucesso!');
        this.messageService.add({ key: 'caixa', severity: 'info', summary: 'Informação', detail: 'Quantidade alterada com sucesso!' });
    }

    onRowEditCancel(product: any, index: number) {
        product.qtde = product.qtdeOld;
    }

    getSubTotal() {
        let total = 0;

        this.products.filter(q => q.removido !== true).forEach(q => {
            total += (q.qtde * q.preco) - q.desconto;
        });
        return total;
    }

    getTotalItens() {
        let itens = 0;
        this.products.filter(q => q.removido !== true).forEach(q => {
            itens += 1;
        });

        return itens;
    }


    search(event: any) {
        if (!event) {
            alert('Não foi possível ler o código de barras, tente novamente.');
            return;
        }
        this.codigoBarras = event.value;
        this.getByCodigoBarras();
    }

    blurOn(event: any) {
        this.getByCodigoBarras();
    }

    getImageProduct() {
        if (this.product && this.product.urlArquivo)
            return this.baseImg + '/' + this.product.urlArquivo;

        return this.baseImg + '/Comuns/Images/produto-sem-img.png';
    }

    getByCodigoBarras() {

        if (!this.codigoBarras) return;

        this.loading = true;
        this.produtoService.getByCodigoBarras(this.codigoBarras)
            .then(res => {
                res.subscribe((r: any) => {
                    this.loading = false;
                    if (r.sucesso && r.conteudo) {
                        let produto = r.conteudo as ProdutoVendaModel;
                        if (!produto.id || produto.id == 0) {
                            //this.appComponent.showErrorMessage('Produto não encontrado com o código: ' + this.codigoBarras);
                            this.messageService.add({ key: 'caixa', severity: 'warn', summary: 'Ops', detail: 'Produto não encontrado com o código: ' + this.codigoBarras });
                            return;
                        }

                        this.addProdutoVendaModal(r.conteudo as ProdutoVendaModel);
                    }
                    else {
                        // this.appComponent.showErrorMessage('Não foi possível recuperar o produto.', r.mensagemRetorno);
                        this.messageService.add({ key: 'caixa', severity: 'error', summary: 'Error', detail: 'Não foi possível recuperar o produto.' });
                    }
                });
            });
    }


    addProdutoVendaModal(produto: ProdutoVendaModel) {
        if (!produto) return;

        if (!produto.id || produto.id == 0) {
            //this.appComponent.showErrorMessage('Produto não encontrado com o código: ' + this.codigoBarras);
            this.messageService.add({ key: 'caixa', severity: 'error', summary: 'Atenção', detail: 'Produto não encontrado com o código: ' + this.codigoBarras });
            return;
        }

        beep();
        produto.qtde = 1;
        produto.qtdeOld = 1;
        produto.total = produto.qtde * produto.preco;
        produto.ordem = this.products.length + 1;
        produto.removido = false;
        produto.desconto = 0;
        produto.acrescimo = 0;
        this.product = produto;
        this.products.push(produto);
        this.codigoBarras = "";
        this.orderProduts();
    }

    deleteConfirm(event: Event, record: ProdutoVendaModel) {

        this.confirmationService.confirm({
            target: event.target as EventTarget,
            header: 'Confirmação',
            message: 'Deseja remover o item ' + record.nome + ' ?',
            icon: 'pi pi-exclamation-triangle',
            rejectLabel: 'não',
            rejectIcon: 'pi pi-times',
            acceptLabel: 'sim',
            acceptIcon: 'pi pi-check',
            defaultFocus: 'reject',
            acceptButtonStyleClass: 'p-button-danger',
            rejectButtonStyleClass: 'p-button-primary',
            accept: () => {
                //confirm action
                this.delete(record);
            },
            reject: () => {
                //reject action
            }
        });
    }

    delete(record: ProdutoVendaModel) {

        record.removido = true;
        beepDelete();
        //this.products = this.products.filter(q => q.id != record.id);
        // this.appComponent.showMessage(`Item ${this.product.nome} foi removido com sucesso!`);

        this.messageService.add({ key: 'caixa', severity: 'info', summary: 'Item Removido', detail: `Item ${this.product.nome} foi removido com sucesso!` });
    }

    goHome() {
        this.router.navigate(['/']);
    }
    sair() {
        this.router.navigate(['/login']);
    }

    orderProduts() {
        this.products = this.products.sort((a, b) => {
            if (a.ordem > b.ordem) {
                return -1;
            }
            if (a.ordem < b.ordem) {
                return 1;
            }
            return 0;
        });
    }

    @HostListener('window:keyup', ['$event'])
    keyEvent(event: KeyboardEvent) {
        let key = event.key;
        if (key == 'F1') {
            this.showFinalizarCompra();
        }
        else if (key == 'F2') {
            this.showPesquisarProduto();
        }
        else if (key == 'F4') {
            this.showCancelarCompra();
        }
        else if (key == 'F8') {
            this.showAbrirFecharCaixa();
        }
    }

    showFinalizarCompra() {

        this.valorTotal = this.getSubTotalFinalizarVenda();
        if (this.getTotalItens() == 0) {
            alert('Insira produtos para finalizar a venda!')
            return;
        }

        this.guid = this.newGuid();
        this.displayModalFinalizarCompra = true;
        this.valorTroco = 0;
        this.valorRecebido = undefined;
        this.getProdutosFinalizarVenda();


        setTimeout(() => {
            if (this.vlrRecebidoElement) {
                if (this.vlrRecebidoElement.input) this.vlrRecebidoElement.input.nativeElement.focus()
                else if (this.vlrRecebidoElement.nativeElement) this.vlrRecebidoElement.nativeElement.focus()
            }
        }, 500);

        this.recuperarClientes();

    }

    onRowSelect(event: any, op: any) {
        console.log(event);
        op.hide();
    }

    removerCliente(event: Event) {
        this.cliente = new ClienteModel();
    }
    pesquisarCliente(event: Event) {
        this.ref = this.dialogService.open(ClienteComponent, {
            data: 'CAIXA',
            header: 'Selecione o cliente',
            contentStyle: { "overflow": "auto" },
            maximizable: true,
            width: '90%'
        });

        let dialogRef: any = this.dialogService.dialogComponentRefMap.get(this.ref);
        dialogRef.changeDetectorRef.detectChanges();
        let instance = dialogRef.instance.componentRef.instance as ClienteComponent;
        instance.onCloseDialog.subscribe((cliente: any) => {

            if (!cliente) return;
            this.cliente = cliente;

            // this.addProdutoVendaModal(produto as ProdutoVendaModel);
            // this.ref?.close();

        });

    }

    recuperarClientes() {
        let cliente = new ClienteModel();
        cliente.ativo = true;

        this.clienteService.getClientes(cliente)
            .then(res => res.subscribe((r: any) => {
                this.clientes = r.conteudo.items;
            }))
            .catch((erro) => {
                this.messageService.add({
                    key: 'caixa',
                    severity: 'erro',
                    summary: 'Erro ao consultar clientes',
                    detail: `Não foi possível consultar clientes`
                });
            });

    }

    getProdutosFinalizarVenda() {
        this.produtosFinalizaVenda = this.products.map(object => ({ ...object }));
        this.produtosFinalizaVenda = this.produtosFinalizaVenda.filter(q => q.removido !== true);
    }

    getSubTotalFinalizarVenda() {
        let total = 0;

        this.produtosFinalizaVenda.filter(q => q.removido != true).forEach(q => {
            total += (q.qtde * q.preco) - q.desconto;
        });
        return total;
    }

    onChangeRecebimento(valorRecebido: any) {
        let desconto = this.valorDesconto ? this.valorDesconto : 0;

        this.valorTroco = (this.valorRecebido! - (desconto) + this.valorAcrescimo) - this.getSubTotalFinalizarVenda();
        this.valorRecebido = valorRecebido;
    }

    onChangeDesconto(valorDesconto: any) {
        this.valorTroco = (this.valorRecebido! - this.valorDesconto! + this.valorAcrescimo) - this.getSubTotalFinalizarVenda();
        this.valorDesconto = valorDesconto;
    }

    concluirVenda(event: Event) {

        this.executandoVenda = true;
        this.pedido.itensPedido = [];
        this.pedido.idCliente = null;
        this.pedido.idUsuario = this.authenticationService.userValue.id;
        this.pedido.codigo = this.guid;
        this.pedido.descricao = this.observacao!;
        this.pedido.valorPedido = this.getSubTotalFinalizarVenda()!;
        this.pedido.valorDesconto = this.valorDesconto!;
        this.pedido.valorAcrescimo = this.valorAcrescimo!
        this.pedido.valorTotal = this.pedido.valorPedido - this.pedido.valorDesconto + this.pedido.valorAcrescimo;

        this.pedido.formaPagamentoStr = this.selectedFormaPagto;


        this.products.forEach(produto => {
            let item = new ItemPedidoModel();
            item.idPedido = this.pedido.id;
            item.idProduto = produto.id;
            item.descricao = produto.nome!;
            item.quantidade = produto.qtde;
            item.valorUnitario = produto.preco;
            item.valorDesconto = produto.desconto;
            item.valorAcrescimo = produto.acrescimo;
            this.pedido.itensPedido.push(item);
        });

        this.vendaService.criarPedido(this.pedido)
            .then((res: any) => {
                res.subscribe((r: any) => {
                    this.executandoVenda = false;
                    this.limparComponente();
                    if (r && r.sucesso === true) {
                        this.pedido.id = r.conteudo.id;
                        this.messageService.add({ key: 'caixa', severity: 'success', summary: 'Sucesso', detail: 'Venda realizada com sucesso!' });
                        //this.limparComponente();
                        this.confirmImprimirCupom(event);
                    }
                    else {
                        this.messageService.add({ key: 'caixa', severity: 'error', summary: 'Atenção', detail: 'Não foi possível registrar a venda.' });
                    }
                });
            });
    }

    limparComponente() {
        this.products = [];
        this.product = new ProdutoVendaModel();
        this.displayModalFinalizarCompra = false;
        this.displayImprimirCupom = false;
        this.codigoBarras = null;

        setTimeout(() => {
            if (this.codigoBarraElement) {
                if (this.codigoBarraElement.input) this.codigoBarraElement.input.nativeElement.focus()
                else if (this.codigoBarraElement.nativeElement) this.codigoBarraElement.nativeElement.focus()
            }
        }, 500);

    }

    showAbrirFecharCaixa() {
        alert('Ainda não implementado!')
    }
    showCancelarCompra() {
        alert('Ainda não implementado!')
       // this.displayModalCancelarCompra = true;
    }

    showPesquisarProduto() {

        this.ref = this.dialogService.open(ProdutoComponent, {
            data: 'CAIXA',
            header: 'Selecione o produto',
            contentStyle: { "overflow": "auto" },
            maximizable: true,
            width: '90%'
        });

        let dialogRef: any = this.dialogService.dialogComponentRefMap.get(this.ref);
        dialogRef.changeDetectorRef.detectChanges();
        let instance = dialogRef.instance.componentRef.instance as ProdutoComponent;
        instance.onCloseDialog.subscribe((produto: any) => {

            this.addProdutoVendaModal(produto as ProdutoVendaModel);

            // this.addProdutoVendaModal(produto as ProdutoVendaModel);
            // this.ref?.close();

        });

        // instance.onMaximizeDialog.subscribe(() => {
        //     console.log('onMaximize')
        // });

    }

    newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0,
                v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    confirmImprimirCupom(event: Event) {

        this.displayImprimirCupom = true;
        // this.confirmationService.confirm({
        //     target: event.target as EventTarget,       
        //     header: 'Confirmação',
        //     message: 'Gostaria de imprimir o cupom fiscal?',
        //     icon: 'pi pi-exclamation-triangle',
        //     rejectLabel: 'não',
        //     rejectIcon: 'pi pi-times',
        //     acceptLabel: 'sim',
        //     acceptIcon: 'pi pi-check',
        //     defaultFocus: 'reject',
        //     acceptButtonStyleClass: 'p-button-danger',
        //     rejectButtonStyleClass: 'p-button-primary',
        //     accept: () => {
        //         this.ImprimirCupom();
        //     },
        //     reject: () => {
        //         this.limparComponente();
        //     },
        //     key: 'positionDialog'
        // });

    }

    ConcluirComCupom() {


        this.cupomFiscalModel = new CupomFiscalModel();
        this.cupomFiscalModel.data = this.time;
        this.cupomFiscalModel.caixa = this.nome!;
        this.cupomFiscalModel.nomeCliente = this.cliente?.nome!;
        this.cupomFiscalModel.cpfcnpjCliente = this.cliente?.cpj_cnpj!;
        this.cupomFiscalModel.valorDesconto = this.valorDesconto!;
        this.cupomFiscalModel.valorAcrescimo = this.valorAcrescimo!;
        this.cupomFiscalModel.valorRecebido = this.valorRecebido!;
        this.cupomFiscalModel.valorTroco = this.valorTroco!;
        this.cupomFiscalModel.subTotalVenda = this.getSubTotalFinalizarVenda();
        this.cupomFiscalModel.formaPagto = this.selectedFormaPagto!;
        this.cupomFiscalModel.observacao = this.observacao!;
        this.cupomFiscalModel.idPedido = this.pedido.id!;
        this.cupomFiscalModel.guid = this.guid;
        this.cupomFiscalModel.produtos = this.produtosFinalizaVenda!

        localStorage.setItem('cupom-produtosFinalizaVenda', JSON.stringify(this.cupomFiscalModel));

        window.open("/caixa/cupom", "_blank");

        // window.location.href = "/caixa/cupom";
        //this.router.navigate([`/caixa/cupom`]);
        this.limparComponente();


        //window.print();
    }

    ConcluirSemCupom() {
        this.limparComponente();
    }
}

