import { Component, OnInit, AfterViewInit } from '@angular/core';
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
import { UsuarioModel } from '../../usuarios/model/usuarios-model';
import { PagedInputDTO } from 'src/app/shared/paginator/pagedInputDTO';
import { UsuarioService } from '../../usuarios/usuario.service';
import { AppComponent } from 'src/app/app.component';
import { Router } from '@angular/router';
import { FornecedorModel } from '../model/fornecedor-model';
import { ListarComumComponent } from 'src/app/shared/utils/comum.component';
import { FornecedorService } from '../fornecedor.service';

@Component({
  selector: 'app-fornecedor',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],

  templateUrl: './fornecedor.component.html',
  styleUrls: ['./fornecedor.component.scss'],
})
export class FornecedorComponent extends ListarComumComponent implements OnInit, AfterViewInit {

  tituloPagina: string = 'FORNECEDOR';
  urlBase: string = 'fornecedor';

  fornecedores: FornecedorModel[] = [];
  loading!: boolean;
  tableFilter: boolean = true;

  constructor(
    private fornecedorService: FornecedorService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private appComponent: AppComponent
  ) {
    super();
  }

  override ngOnInit(): void {
  }

  override ngAfterViewInit() { }

  override ngOnDestroy() { }

  novo_click() {
    this.router.navigate([`${this.urlBase}/0`]);
  }

  tableFilterAvancado() {
    this.tableFilter = !this.tableFilter;
    return this.tableFilter;
  }

  loadRecords(event?: FornecedorModel) {

    if (event === undefined)
      event = new FornecedorModel();

    this.loading = true;
    this.fornecedorService.getPaged(event)
      .then((res: any) => {
        res.subscribe((r: any) => {
          this.loading = false;
          if (r && r.sucesso === true && r.conteudo != null) {
            this.fornecedores = r.conteudo.items;
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

  edit(record: FornecedorModel) {
    this.router.navigate([`${this.urlBase}/${record.id}`]);
  }

  deleteConfirm(event: Event, record: FornecedorModel) {

    this.confirmationService.confirm({
      target: event.target as EventTarget,
      header: 'Confirmação',
      message: 'Deseja remover o usuário ' + record.razaoSocial + ' ?',
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

  delete(record: FornecedorModel) {

    this.fornecedorService.delete(record.id)
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
}
