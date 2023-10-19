
import { ActivatedRoute, Router } from '@angular/router';
import { PerfisService } from '../perfis.service';
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
import { PerfilEntidade } from '../entidade/perfil-entidade';
import { PagedInputDTO } from 'src/app/shared/paginator/pagedInputDTO';
import { ConfirmationService } from 'primeng/api';
import { AppComponent } from 'src/app/app.component';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-perfis',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],        // end of trigger block

  templateUrl: './perfis.component.html',
  styleUrls: ['./perfis.component.scss'],
})
export class PerfisComponent implements OnInit, AfterViewInit {

  perfis: PerfilEntidade[] = [];
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
    private perfisService: PerfisService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
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

  loadCustomers(event: PagedInputDTO) {
    this.loading = true;
    this.perfisService.getPerfilsPaged(event)
      .then(res => {
        res.subscribe((r: any) => {
          this.loading = false;
          if (r.conteudo != null) {
            this.perfis = r.conteudo.items;
            this.totalRecords = r.conteudo.totalCount;
          }
        })
      })
      .catch((error) => {

      })
      .finally(() => {

      });
  }

  edit(perfil: PerfilEntidade) {
    this.router.navigate([`perfis/${perfil.id}`]);
  }

  deleteConfirm(event: Event, perfil: PerfilEntidade) {

    this.confirmationService.confirm({
      target: event.target as EventTarget,
      header: 'Confirmação',
      message: 'Deseja remover o perfil ' + perfil.nome + ' ?',
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
        this.delete(perfil);
      },
      reject: () => {
        //reject action
      }
    });
  }

  delete(perfil: PerfilEntidade) {

    this.perfisService.delete(perfil.id)
      .then((q: any) => {

        q.subscribe((res: any) => {
          if (res && res.sucesso) {
            this.appComponent.showMessage('Registro removido com sucesso!');
          } else {
            console.log(res.mensagemRetorno)
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

  onSelectAllChange(event: any) {
    const checked = event.checked;
   
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
  clear(table: Table) {
    table.clear();
  }

}
