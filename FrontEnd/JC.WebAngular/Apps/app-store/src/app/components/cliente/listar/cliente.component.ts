import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
  // ...
} from '@angular/animations';
import { SelectItem } from 'primeng/api';
import { PagedInputDTO } from 'src/app/shared/paginator/pagedInputDTO';
import { ConfirmationService } from 'primeng/api';
import { AppComponent } from 'src/app/app.component';
import { ClienteModel } from '../model/cliente.model';
import { ClienteService } from '../cliente.service';
import { Table } from 'primeng/table';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-cliente',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],        // end of trigger block

  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.scss'],
})
export class ClienteComponent implements OnInit, AfterViewInit {
  usuarios: ClienteModel[] = [];
  loading!: boolean;


  totalRecords!: number;
  cols: any[] = [];
  representatives: any[] = [];
  selectAll: boolean = false;
  selectedCustomers: any[] = [];

  // pt!: any;
  // matchModeOptions!: SelectItem[];

  pesquisaCliente: boolean = false;
  onCloseDialog!: Observable<any>;

  constructor(
    private service: ClienteService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private appComponent: AppComponent,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig
  ) {
    this.onCloseDialog = this.ref.onClose;
   }

  ngOnInit() {
    this.pesquisaCliente = this.config.data != undefined;
  }

  ngAfterViewInit() { }

  ngOnDestroy() { }

  loadCustomers(event?: PagedInputDTO) {

    if (event === undefined)
      event = new PagedInputDTO();

    this.loading = true;
    this.service.getUsuarioPaged(event)
      .then((res: any) => {
        res.subscribe((r: any) => {
          this.loading = false;
          if (r && r.sucesso === true && r.conteudo != null) {
            this.usuarios = r.conteudo.items;
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

  edit(usuario: ClienteModel) {
    this.router.navigate([`cliente/${usuario.id}`]);
  }

  deleteConfirm(event: Event, usuario: ClienteModel) {

    this.confirmationService.confirm({
      target: event.target as EventTarget,
      header: 'Confirmação',
      message: 'Deseja remover o usuário ' + usuario.nome + ' ?',
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
        this.delete(usuario);
      },
      reject: () => {
        //reject action
      }
    });
  }

  delete(usuario: ClienteModel) {

    this.service.delete(usuario.id)
      .then((q: any) => {

        q.subscribe((res: any) => {
          if (res && res.sucesso) {
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
    this.selectAll = value.length === this.totalRecords;
    this.selectedCustomers = value;
  }

  onSelectAllChange(event: any) { }


  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
  clear(table: Table) {
    table.clear();
  }

  obterDescricaoSituacaoAtual(record: ClienteModel) {
    return (record.ativo) ? 'Ativo' : 'Inativo';
  }

  selectCliente(produto: ClienteModel) {
    this.ref.close(produto);
  }
}
