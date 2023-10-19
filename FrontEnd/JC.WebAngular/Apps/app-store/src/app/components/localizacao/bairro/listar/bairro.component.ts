
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
import { PerfisService } from '../../../perfis/perfis.service';
import { BairroModel, PerfilEntidade } from '../model/bairro.model';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';

@Component({
  selector: 'app-bairro',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],        // end of trigger block

  templateUrl: './bairro.component.html',
  styleUrls: ['./bairro.component.scss'],
})
export class BairroComponent implements OnInit, AfterViewInit {

  records: PerfilEntidade[] = [];
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
    private localizacaoService: LocalizacaoService,
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
    this.localizacaoService.getBairroPaged(event)
      .then(res => {
        res.subscribe((r: any) => {
          this.loading = false;
          if (r && r.sucesso) {
            this.records = r.conteudo.items;
            this.totalRecords = r.conteudo.totalCount;
          } else {
            console.log(r.mensagemRetorno)
            this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', r.mensagemRetorno);
          }
        })
      });
  }

  edit(record: BairroModel) {
    this.router.navigate([`bairro/${record.id}`]);
  }

  deleteConfirm(event: Event, record: BairroModel) {

    this.confirmationService.confirm({
      target: event.target as EventTarget,
      header: 'Confirmação',
      message: 'Deseja remover o perfil ' + record.nome + ' ?',
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

  delete(perfil: BairroModel) {

    this.perfisService.delete(perfil.id)
      .then((q: any) => {

        q.subscribe((res: any) => {
          if (res && res.sucesso) {
            this.appComponent.showMessage('Registro salvo com sucesso!');
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

}
