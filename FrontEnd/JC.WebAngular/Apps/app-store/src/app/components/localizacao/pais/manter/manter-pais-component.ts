import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { Component, OnInit, AfterViewInit, ViewEncapsulation, forwardRef } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
  // ...
} from '@angular/animations';
import { MessageService } from 'primeng/api';
import { ManterComumComponent } from 'src/app/shared/utils/manter-comum.component';
import { PaisModel } from '../model/pais-model';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { PaisService } from '../pais.service';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-manter-pais',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],

  templateUrl: './manter-pais-component.html',
  styleUrls: ['./manter-pais-component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [MessageService]

})
export class ManterPaisComponent extends ManterComumComponent implements OnInit, AfterViewInit {
  tituloPagina: string = 'CADASTRAR PAIS';
  urlBase: string = 'pais';
  record: PaisModel = new PaisModel();
  loading: boolean = false;

  id: number = 0;


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private messageService: MessageService,
    private paisService: PaisService,
    private appComponent: AppComponent
  ) {
    super();
  }

  override  ngOnInit() {

    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (isNaN(Number(this.id))) {
      this.router.navigate(['pais']);
    } else if (this.id == 0) {
      this.record.ativo = true;
    }

    this.getRecord();
  }

  override ngAfterViewInit(): void {
  }

  override ngOnDestroy() { }

  getRecord() {
    this.record = new PaisModel();
    if (this.id == 0) return;

    this.loading = true;
    this.paisService.getPais(this.id)
      .then(res => {
        res.subscribe(r => {
          this.loading = false;
          if (r.sucesso && r.conteudo) {
            this.record = r.conteudo;
          }
          else {
            this.appComponent.showErrorMessage('Não foi possível recuperar os registros.', r.mensagemRetorno);
          }
        });
      });
  }

  salvar_click() {
    this.loading = true;
    this.paisService.savePais(this.record)
      .then((res: any) => {
        res.subscribe((r: any) => {
          this.loading = false;
          if (r && r.sucesso === true) {
            if (this.record.id > 0)
              this.appComponent.showMessage('Registro alterado com sucesso!');
            else
              this.appComponent.showMessage('Registro salvo com sucesso!');

            this.record.id = r.conteudo.id;
          }
          else {
            this.appComponent.showErrorMessage('Não foi possível salvar o registro.', r.mensagemRetorno);
          }
        });
      });
  }

  voltar_click() {
    this.router.navigate([`${this.urlBase}/`]);
  }

}