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
import { MessageService, SelectItem } from 'primeng/api';
import { PagedInputDTO } from 'src/app/shared/paginator/pagedInputDTO';
import { ConfirmationService } from 'primeng/api';
import { AppComponent } from 'src/app/app.component';
import { PerfilEntidade } from '../../perfis/entidade/perfil-entidade';
import { PerfisService } from '../../perfis/perfis.service';
import { UsuarioService } from '../usuario.service';
import { UsuarioModel } from '../model/usuarios-model';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-usuario',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],        // end of trigger block

  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss'],
})
export class UsuarioComponent implements OnInit, AfterViewInit {
  usuarios: UsuarioModel[] = [];
  loading!: boolean;
  tableFilter: boolean = true;

  totalRecords!: number;
  cols: any[] = [];
  representatives: any[] = [];
  selectAll: boolean = false;
  selectedCustomers: any[] = [];

  pt!: any;
  matchModeOptions!: SelectItem[];
  constructor(
    private usuarioService: UsuarioService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private appComponent: AppComponent
  ) { }

  ngOnInit() {
    const customFilterName = 'custom-equals';
    this.pt = {
      matchMode: ['Começa', 'contem', 'nao contem', 'termina com', 'igual', 'diferente', 'sem filtro']
    };

    this.matchModeOptions = [
      { label: 'Contém', value: 'Contém' },
      { label: 'Igual', value: 'Igual' },
      { label: 'Começa com', value: 'Começa com' },
      { label: 'Termina com', value: 'Termina com' },
    ];
  }

  ngAfterViewInit() { }

  ngOnDestroy() { }

  tableFilterAvancado() {
    this.tableFilter = !this.tableFilter;
    return this.tableFilter;
  }

  loadCustomers(event?: PagedInputDTO) {

    if (event === undefined)
      event = new PagedInputDTO();

    this.loading = true;
    this.usuarioService.getUsuarioPaged(event)
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

  edit(usuario: UsuarioModel) {
    this.router.navigate([`usuarios/${usuario.id}`]);
  }

  deleteConfirm(event: Event, usuario: UsuarioModel) {

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

  delete(usuario: UsuarioModel) {

    this.usuarioService.delete(usuario.id)
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
  
}
