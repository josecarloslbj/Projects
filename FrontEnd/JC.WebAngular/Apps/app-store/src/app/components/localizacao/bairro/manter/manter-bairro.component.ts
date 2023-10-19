import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import {
    trigger,
    state,
    style,
    animate,
    transition,
    // ...
} from '@angular/animations';
import { MessageService } from 'primeng/api';
import { AppComponent } from 'src/app/app.component';
import { ProdutoModel } from '../../../produto/model/produto-model';
import { PaisModel } from '../../pais/model/pais-model';
import { EstadoModel } from '../../estado/model/estado.model';
import { CidadeModel } from '../../cidade/model/cidade.model';
import { BairroModel } from '../model/bairro.model';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';

@Component({
    selector: 'app-manter-produto',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './manter-bairro.component.html',
    styleUrls: ['./manter-bairro.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [MessageService]

})
export class ManterBairroComponent implements OnInit, AfterViewInit {
    tituloPagina: string = 'CADASTRAR BAIRRO';
    urlBase: string = 'bairro';
    baseApi = environment.apiUrl;

    record: BairroModel = new BairroModel();
    id: number = 0;
    loading: boolean = false;

    paises: PaisModel[] = [];
    selectedPais: PaisModel = new PaisModel();

    estados: EstadoModel[] = [];
    selectedEstado: EstadoModel = new EstadoModel();

    cidades: CidadeModel[] = [];
    selectedCidade: CidadeModel = new CidadeModel();

    constructor(private route: ActivatedRoute,
        private router: Router,
        private localizacaoService: LocalizacaoService,
        private appComponent: AppComponent
    ) { }

    ngOnInit() {
        this.record.ativo = true;

        this.id = Number(this.route.snapshot.paramMap.get('id'));
        if (isNaN(Number(this.id))) {
            this.router.navigate(['bairro']);
            return;
        }
        this.getPaises();
    }

    ngAfterViewInit(): void {
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
        this.record = new BairroModel();
        if (this.id == 0) return;

        this.loading = true;
        this.localizacaoService.getBairro(this.id)
            .then(res => {
                res.subscribe(r => {
                    this.loading = false;
                    if (r.sucesso && r.conteudo) {
                        this.record = r.conteudo;

                        var pais = this.paises.find(q => q.id == this.record.cidade?.estado?.idPais);
                        if (pais) {
                            this.selectedPais = pais;
                            this.getEstados(this.selectedPais.id, this.record.cidade?.estado?.id, this.record.cidade?.id);
                        }
                    }
                    else {
                        this.appComponent.showErrorMessage('Não foi possível recuperar os registros.', r.mensagemRetorno);
                    }
                });
            });
    }

    salvar_click() {
        this.loading = true;
        this.localizacaoService.saveBairro(this.record)
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

    getPaises() {
        this.localizacaoService.getPaisPaged(undefined)
            .then(res => {
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

    getEstados(idPais?: number, idEstado?: number, idCidade?: number) {
        this.estados = [];
        let event = new EstadoModel();
        if (idPais) {
            event.idPais = idPais;
        }

        if (idPais && idPais! > 0) {
            let padrao = this.paises.find(q => q.sigla === 'BR');
            if (padrao) {
                this.selectedPais = padrao;
            }
        }

        this.localizacaoService.getEstadosPaged(event)
            .then(res => {
                res.subscribe((r: any) => {
                    if (r && r.sucesso) {
                        this.estados = r.conteudo.items;

                        let padrao: EstadoModel | undefined | null = new EstadoModel();
                        if (idEstado) {
                            padrao = this.estados.find(q => q.id === idEstado);
                        }
                        else {
                            padrao = this.estados.find(q => q.padrao === true);
                        }

                        if (padrao?.id! > 0) {
                            this.selectedEstado = padrao!;
                            this.getCidades(this.selectedEstado.id, idCidade);
                        }

                    } else {
                        this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', r.mensagemRetorno);
                    }
                });
            });
    }

    getCidades(idEstado?: number, idCidade?: number) {
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

                        let padrao: CidadeModel | undefined | null = new CidadeModel();

                        if (idCidade) {
                            padrao = this.cidades.find(q => q.id === idCidade);
                        }
                        else {
                            padrao = this.cidades.find(q => q.padrao === true);
                        }

                        if (padrao) {
                            this.selectedCidade = padrao;
                            this.record.cidade = padrao;
                            this.record.idCidade = padrao.id;
                        }
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

    cidade_onChange(cidade: CidadeModel) {
        this.record.cidade = cidade;
        this.record.idCidade = cidade.id;
        console.log(cidade);
    }

    bairro_onChange(bairro: BairroModel) {
        console.log(bairro);
    }

}  