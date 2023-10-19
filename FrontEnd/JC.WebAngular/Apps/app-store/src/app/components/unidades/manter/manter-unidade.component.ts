import { ActivatedRoute, Router } from '@angular/router';

import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import {
    trigger,
    state,
    style,
    animate,
    transition,
    // ...
} from '@angular/animations';
import { GroupByPipe } from 'src/app/shared/pipes/groupBy';
import { PerfilEntidade } from '../../perfis/entidade/perfil-entidade';
import { PerfisService } from '../../perfis/perfis.service';
import { UsuarioModel } from '../../usuarios/model/usuarios-model';
import { UsuarioService } from '../../usuarios/usuario.service';
import { AppComponent } from 'src/app/app.component';
import { MessageService } from 'primeng/api';
import { UnidadeModel } from '../model/unidade-model';
import { PaisModel } from '../../localizacao/pais/model/pais-model';
import { EstadoModel } from '../../localizacao/estado/model/estado.model';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';
import { CidadeModel } from '../../localizacao/cidade/model/cidade.model';
import { BairroModel } from '../../localizacao/bairro/model/bairro.model';
import { UnidadeService } from '../unidade.service';


@Component({
    selector: 'app-manter-unidade',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './manter-unidade.component.html',
    styleUrls: ['./manter-unidade.component.scss'],
    providers: [GroupByPipe, MessageService]

})
export class ManterUnidadeComponent implements OnInit, AfterViewInit {

    record: UnidadeModel = new UnidadeModel();

    id: number = 0;
    loading: boolean = false;

    status: boolean = true;
    ingredient: string = ' ';

    paises: PaisModel[] = [];
    selectedPais: PaisModel = new PaisModel();

    estados: EstadoModel[] = [];
    selectedEstado: EstadoModel = new EstadoModel();

    cidades: CidadeModel[] = [];
    selectedCidade: CidadeModel = new CidadeModel();

    bairros: BairroModel[] = [];
    selectedBairro: BairroModel = new BairroModel();

    constructor(
        private unidade: UnidadeService,
        private localizacaoService: LocalizacaoService,
        private appComponent: AppComponent,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {
        this.record.ativo = true;
        this.status = true;

        this.id = Number(this.route.snapshot.paramMap.get('id'));
        if (isNaN(Number(this.id))) {
            this.router.navigate(['usuarios']);
            return false;
        }

        this.getPaises();
        return true;
    }

    ngAfterViewInit(): void {
        this.getPaises();
        if (this.id == 0) {
            return;
        }

        setTimeout(() => {
            let padrao = this.paises.find(q => q.sigla === 'BR');
            if (padrao) {
                this.selectedPais = padrao;
            }
            this.getRecord();
        }, 100);
    }

    ngOnDestroy() { }


    getRecord() {
        this.record = new UnidadeModel();
        if (this.id == 0) return;

        this.loading = true;
        this.unidade.get(this.id)
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
        this.unidade.save(this.record)
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
                        if (padrao) {
                            this.selectedEstado = padrao;
                            this.getCidades(this.selectedEstado.id);
                        }

                    } else {
                        this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', r.mensagemRetorno);
                    }
                });
            });
    }

    getCidades(idEstado?: number) {
        this.cidades = [];
        let event = new CidadeModel();
        if (idEstado) {
            event.idEstado = idEstado;
        }

        this.localizacaoService.getCidadesPaged(event)
            .then(res => {
                res.subscribe((r: any) => {
                    if (r && r.sucesso) {
                        this.cidades = r.conteudo.items;

                        let padrao = this.cidades.find(q => q.padrao === true);
                        if (padrao)
                            this.selectedCidade = padrao;

                    } else {
                        this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', r.mensagemRetorno);
                    }
                });
            });
    }

    getBairros(idCidade?: number) {
        this.bairros = [];
        let event = new BairroModel();
        if (idCidade) {
            event.idCidade = idCidade;
        }

        this.localizacaoService.getBairroPaged(event)
            .then(res => {
                res.subscribe((r: any) => {
                    if (r && r.sucesso) {
                        this.bairros = r.conteudo.items;
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
        this.getCidades(estado.id);
    }

    cidade_onChange(cidade: EstadoModel) {
        this.getBairros(cidade.id);
    }

    bairro_onChange(bairro: BairroModel) {
        this.record.bairro = bairro;
        this.record.idBairro = bairro.id;
    }

}  