import { ActivatedRoute, Router } from '@angular/router';

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
import { ManterComumComponent } from 'src/app/shared/utils/manter-comum.component';
import { AppComponent } from 'src/app/app.component';
import { GrupoProdutoModel } from '../model/grupo-produto-model';
import { GrupoProdutoService } from '../grupo-produto.service';


@Component({
    selector: 'app-manter-grupoproduto',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './manter-grupo-produto.component.html',
    styleUrls: ['./manter-grupo-produto.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [MessageService]

})
export class ManterGrupoProdutoComponent extends ManterComumComponent implements OnInit, AfterViewInit {
    tituloPagina: string = 'CADASTRAR GRUPO PRODUTOS';
    urlBase: string = 'grupoproduto';

    record: GrupoProdutoModel = new GrupoProdutoModel();
    id: number = 0;
    loading: boolean = false;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private grupoProdutoService: GrupoProdutoService,
        private appComponent: AppComponent
    ) {
        super();
    }

    override ngOnInit() {

        this.id = Number(this.route.snapshot.paramMap.get('id'));
        if (isNaN(Number(this.id))) {
            this.router.navigate([`${this.urlBase}`]);
        }
        this.getFornecedor();
    }

    override ngAfterViewInit(): void {

    }
    override ngOnDestroy() { }

    async getFornecedor() {
        this.record = new GrupoProdutoModel();
        if (this.id == 0) return;

        this.loading = true;
        await this.grupoProdutoService.get(this.id)
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
    
    async salvar_click() {

        this.loading = true;
        await this.grupoProdutoService.save(this.record)
            .then((res: any) => {
                res.subscribe((r: any) => {
                    this.loading = false;
                    if (r && r.sucesso === true) {
                        this.appComponent.showMessage('Registro salvo com sucesso!');
                    }
                    else {
                        this.appComponent.showErrorMessage('Não foi possível salvar o registro.', r.mensagemRetorno);
                    }
                });
            });
    }

    async voltar_click(){
        this.router.navigate([`${this.urlBase}/`]);
    }
}  