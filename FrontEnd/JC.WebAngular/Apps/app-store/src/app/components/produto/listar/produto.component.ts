import { Component, OnInit, AfterViewInit, Injectable, Input } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
  // ...
} from '@angular/animations';
import { MessageService, SelectItem } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { AppComponent } from 'src/app/app.component';
import { Router } from '@angular/router';
import { ListarComumComponent } from 'src/app/shared/utils/comum.component';
import { ProdutoModel } from '../model/produto-model';
import { ProdutoService } from '../produto.service';
import { DialogService, DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Observable } from 'rxjs';
import { Table } from 'primeng/table';

@Injectable({
  providedIn: 'root'
})
@Component({
  selector: 'app-produto',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],

  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.scss'],
  providers: []
})
export class ProdutoComponent extends ListarComumComponent implements OnInit, AfterViewInit {

  tituloPagina: string = 'PRODUTO';
  urlBase: string = 'produto';

  records: ProdutoModel[] = [];
  loading!: boolean;
  tableFilter: boolean = true;

  pesquisaProduto: boolean = false;
  onCloseDialog!: Observable<any>;
  //onMaximizeDialog!: Observable<any>;

  constructor(
    private produtoService: ProdutoService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private appComponent: AppComponent,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig
  ) {
    super();


    this.onCloseDialog = this.ref.onClose;
    //  this.onMaximizeDialog = this.ref.onMaximize;
  }

  override ngOnInit(): void {
    this.pesquisaProduto = this.config.data != undefined;    
  }

  override ngAfterViewInit() {

  }

  override ngOnDestroy() {
   
  }

  novo_click() {
    this.router.navigate([`${this.urlBase}/0`]);
  }

  loadRecords(event?: ProdutoModel) {

    if (event === undefined)
      event = new ProdutoModel();

    this.loading = true;
    this.produtoService.getPaged(event)
      .then((res: any) => {
        res.subscribe((r: any) => {
          this.loading = false;
          if (r && r.sucesso === true && r.conteudo != null) {
            this.records = r.conteudo.items;
            this.totalRecords = r.conteudo.totalCount;
          } else {
            this.appComponent.showErrorMessage('Não foi possível recuperar os registros.', r.mensagemRetorno);
          }
        },
          () => {
            this.loading = true;
            this.appComponent.showErrorMessage('Não foi possível recuperar os registros.');
          })
      })
      ;
  }

  edit(record: ProdutoModel) {
    this.router.navigate([`${this.urlBase}/${record.id}`]);
  }

  deleteConfirm(event: Event, record: ProdutoModel) {

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

  delete(record: ProdutoModel) {

    this.produtoService.delete(record.id)
      .then((q: any) => {

        q.subscribe((res: any) => {
          if (res && res.sucesso) {
            this.loadRecords();
            this.appComponent.showMessage('Registro removido com sucesso!');
          } else {
            this.appComponent.showErrorMessage('Não foi possível deletar o registro.', res.mensagemRetorno);
          }
        })
      })
      .catch(error => {
        this.appComponent.showErrorMessage('Não foi possível deletar o Registro!', error, 'Ops...');
      });

  }
  
  onSelectionChange(value = []) {
  }

  onSelectAllChange(event: any) { }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
  clear(table: Table) {
    table.clear();
  }

  getCaixaPesquisaProduto(): boolean {
    return this.pesquisaProduto;
  }

  setCaixaPesquisaProduto(value: any) {
    this.pesquisaProduto = true;
  }

  selectProduct(produto: ProdutoModel) {
    this.ref.close(produto);
  }
}

