
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
import { ConfirmationService } from 'primeng/api';
import { AppComponent } from 'src/app/app.component';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';
import { Table } from 'primeng/table';
import { CidadeModel } from '../model/cidade.model';
import { PaisModel } from '../../pais/model/pais-model';
import { EstadoModel } from '../../estado/model/estado.model';


@Component({
  selector: 'app-cidade',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],        // end of trigger block

  templateUrl: './cidade.component.html',
  styleUrls: ['./cidade.component.scss'],
})
export class CidadeComponent implements OnInit, AfterViewInit {

  records: CidadeModel[] = [];
  loading!: boolean;
  tableFilter: boolean = true;

  totalRecords!: number;
  cols: any[] = [];
  representatives: any[] = [];
  selectAll: boolean = false;
  selectedCustomers: any[] = [];

  pt!: any;
  matchModeOptions!: SelectItem[];

  paises: PaisModel[] = [];
  selectedPais: PaisModel = new PaisModel();

  estados: EstadoModel[] = [];
  selectedEstado: EstadoModel = new EstadoModel();


  constructor(
    private localizacaoService: LocalizacaoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private appComponent: AppComponent
  ) { }

  ngOnInit() {
    this.selectedEstado.id = 13;
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

    this.getPaises();
  }

  ngAfterViewInit() { }

  ngOnDestroy() { }

  tableFilterAvancado() {
    this.tableFilter = !this.tableFilter;
    return this.tableFilter;
  }

  loadCustomers(event: CidadeModel) {
    if (this.selectedEstado) {
      event.idEstado = this.selectedEstado.id;
    }
    this.loading = true;
    this.localizacaoService.getCidadesPaged(event)
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

  edit(perfil: CidadeModel) {
    this.router.navigate([`perfis/${perfil.id}`]);
  }

  deleteConfirm(event: Event, perfil: CidadeModel) {

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

  delete(perfil: CidadeModel) {

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


  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  clear(table: Table) {
    table.clear();
  }

  pais_onChange(pais: any) {
    this.getEstados(pais.id);
  }

  getPaises() {

    this.localizacaoService.getPaisPaged(undefined)
      .then(res => {
        this.paises = [];
        res.subscribe((r: any) => {
          if (r && r.sucesso) {
            this.paises = r.conteudo.items;
            if (this.paises != null) {
              let padrao = this.paises.find(q => q.sigla === 'BR');
              if (padrao) {
                this.selectedPais = padrao;
                this.getEstados(this.selectedPais.id);
              }
            }
          } else {
            this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', r.mensagemRetorno);
          }
        })
      });
  }


  getEstados(idPais?: number) {
    this.estados = [];
    let event = new EstadoModel();
    if (idPais) {
      event.idPais = idPais;
    }

    this.localizacaoService.getEstadosPaged(event)
      .then(res => {
        res.subscribe((r: any) => {
          if (r && r.sucesso) {
            this.estados = r.conteudo.items;
            let padrao = this.estados.find(q => q.padrao === true);
            if (padrao) {
              this.selectedEstado = padrao;              
            }


          } else {
            this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', r.mensagemRetorno);
          }
        });
      });
  }

  getNomeEstado(idEstado: number) {
    return this.estados.filter(q => q.id == idEstado).map(q => q.nome);
  }

}
