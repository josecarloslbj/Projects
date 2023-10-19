
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
import { EstadoModel } from '../model/estado.model';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';

@Component({
  selector: 'app-estado',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],        // end of trigger block

  templateUrl: './estado.component.html',
  styleUrls: ['./estado.component.scss'],
})
export class EstadoComponent implements OnInit, AfterViewInit {

  records: EstadoModel[] = [];
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
    this.localizacaoService.getEstadosPaged(event)
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

  edit(perfil: EstadoModel) {
    this.router.navigate([`estados/${perfil.id}`]);
  }

  deleteConfirm(event: Event, perfil: EstadoModel) {

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

  delete(perfil: EstadoModel) {

    // this.perfisService.delete(perfil.id)
    //   .then((q: any) => {

    //     q.subscribe((res: any) => {
    //       if (res && res.sucesso) {
    //         this.appComponent.showMessage();
    //       } else {
    //         console.log(res.mensagemRetorno)
    //         this.appComponent.showErrorMessage('Não foi possível deletar o registro.', res.mensagemRetorno);
    //       }
    //     })
    //   })
    //   .catch(error => {
    //     this.appComponent.showErrorMessage('Não foi possível deletar o Registro!', error, 'Ops...');
    //   });

  }

  onSelectionChange(value = []) {
    this.selectAll = value.length === this.totalRecords;
    this.selectedCustomers = value;
  }
  onSelectAllChange(event: any) {
    const checked = event.checked;
  }

  getNomePais(estado: EstadoModel) {
    console.log(estado);
    if (estado && estado.pais)
      return estado.pais.descricao;
    else
      return '';
  }
}
