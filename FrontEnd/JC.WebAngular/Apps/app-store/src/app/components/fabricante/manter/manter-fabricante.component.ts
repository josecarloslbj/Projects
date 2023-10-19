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
import { FabricanteService } from '../fabricante.service';
import { FabricanteModel } from '../model/fabricante-model';

@Component({
    selector: 'app-manter-fabricante',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './manter-fabricante.component.html',
    styleUrls: ['./manter-fabricante.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [MessageService]

})
export class ManterFabricanteComponent extends ManterComumComponent implements OnInit, AfterViewInit {
    tituloPagina: string = 'CADASTRAR FABRICANTE';
    urlBase: string = 'fabricante';

    record: FabricanteModel = new FabricanteModel();
    id: number = 0;
    loading: boolean = false;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private fabricanteService: FabricanteService,
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
        this.record = new FabricanteModel();
        if (this.id == 0) return;

        this.loading = true;
        await this.fabricanteService.get(this.id)
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
        await this.fabricanteService.save(this.record)
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