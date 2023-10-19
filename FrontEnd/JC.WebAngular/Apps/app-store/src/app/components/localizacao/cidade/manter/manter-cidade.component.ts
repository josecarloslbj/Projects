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
import { GroupByPipe } from 'src/app/shared/pipes/groupBy';
import { Message, MessageService } from 'primeng/api';
import { first } from 'rxjs';
import { PerfisService } from '../../../perfis/perfis.service';
import { CidadeModel } from '../model/cidade.model';
import { PaisModel } from '../../pais/model/pais-model';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';
import { AppComponent } from 'src/app/app.component';
import { EstadoModel } from '../../estado/model/estado.model';
import { PagedInputDTO } from 'src/app/shared/paginator/pagedInputDTO';


@Component({
  selector: 'app-manter-cidade',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],

  templateUrl: './manter-cidade.component.html',
  styleUrls: ['./manter-cidade.component.scss'],
  providers: []

})
export class ManterCidadeComponent implements OnInit, AfterViewInit {
  msgs: Message[] = [];

  record: CidadeModel = new CidadeModel();
  id: number = 0;
  loading: boolean = false;

  paises: PaisModel[] = [];
  selectedPais: PaisModel = new PaisModel();

  estados: EstadoModel[] = [];
  selectedEstado: EstadoModel = new EstadoModel();

  constructor(
    private localizacaoService: LocalizacaoService,
    private route: ActivatedRoute,
    private router: Router,
    private appComponent: AppComponent
  ) { }

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (isNaN(Number(this.id))) {
      this.router.navigate(['cidade']);
    }

    this.getPaises();
  }

  ngAfterViewInit(): void {
    this.getPaises();
  }

  ngOnDestroy() { }

  async salvar_click() {

  }

  getPaises() {
    this.localizacaoService.getPaisPaged(undefined)
      .then(res => {
        res.subscribe((r: any) => {
          if (r && r.sucesso) {
            this.paises = r.conteudo.items;
            if (this.paises != null) {
              let padrao = this.paises.find(q => q.nome === 'BRAZIL');
              if (padrao)
                this.selectedPais = padrao;
                this.getEstados(this.selectedPais.id);
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
            if (padrao)
              this.selectedEstado = padrao;             

          } else {
            this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', r.mensagemRetorno);
          }
        });
      });
  }

  pais_onChange(pais: PaisModel) {
    this.getEstados(pais.id);
  }

  estado_onChange(estado: EstadoModel) {
    console.log(estado);
  }
}

