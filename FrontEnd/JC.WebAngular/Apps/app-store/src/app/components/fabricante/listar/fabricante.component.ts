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
import { Router } from '@angular/router';
import { ListarComumComponent } from 'src/app/shared/utils/comum.component';
import { FabricanteService } from '../fabricante.service';
import { FabricanteModel } from '../model/fabricante-model';

@Component({
  selector: 'app-fabricante',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],

  templateUrl: './fabricante.component.html',
  styleUrls: ['./fabricante.component.scss'],
})
export class FabricanteComponent extends ListarComumComponent implements OnInit, AfterViewInit {

  tituloPagina: string = 'FABRICANTE';
  urlBase: string = 'fabricante';

  records: FabricanteModel[] = [];
  loading!: boolean;
  tableFilter: boolean = true;

  constructor(
    private fabricanteService: FabricanteService,
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

  loadRecords(event?: FabricanteModel) {

    if (event === undefined)
      event = new FabricanteModel();

    this.loading = true;
    this.fabricanteService.getPaged(event)
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

  edit(record: FabricanteModel) {
    this.router.navigate([`${this.urlBase}/${record.id}`]);
  }

  deleteConfirm(event: Event, record: FabricanteModel) {

    this.confirmationService.confirm({
      target: event.target as EventTarget,
      header: 'Confirmação',
      message: 'Deseja remover o usuário ' + record.nome + ' ?',
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

  delete(record: FabricanteModel) {

    this.fabricanteService.delete(record.id)
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
