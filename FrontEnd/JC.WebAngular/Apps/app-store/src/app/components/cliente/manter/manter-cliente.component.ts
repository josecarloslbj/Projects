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
import { AppComponent } from 'src/app/app.component';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ClienteModel } from '../model/cliente.model';
import { ClienteService } from '../cliente.service';
import { UploadComponent } from 'src/app/shared/componentes/upload/upload.component';

@Component({
    selector: 'app-manter-cliente',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './manter-cliente.component.html',
    styleUrls: ['./manter-cliente.component.scss'],
    providers: [GroupByPipe, MessageService]

})
export class ManterClienteComponent implements OnInit, AfterViewInit {

    record: ClienteModel = new ClienteModel();
    id: number = 0;
    loading: boolean = false;

    perfis: PerfilEntidade[] = [];
    selectedPerfis: PerfilEntidade[] = [];


    @ViewChild('appUpload') appUpload!: UploadComponent;

    constructor(
        private service: ClienteService,
        private perfisService: PerfisService,
        private appComponent: AppComponent,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {

        this.id = Number(this.route.snapshot.paramMap.get('id'));
        if (isNaN(Number(this.id))) {
            this.router.navigate(['usuarios']);
        }

        // this.getPerfis();
        this.getRecord();
    }

    ngAfterViewInit(): void {

        if (this.id == 0) {
            this.appUpload.setArquivo(undefined);
        }
    }

    ngOnDestroy() { }

    async getRecord() {
        this.record = new ClienteModel();
        if (this.id == 0) {
            return;
        }

        this.loading = true;
        await this.service.getUsuario(this.id)
            .then(res => {
                res.subscribe(r => {
                    this.loading = false;
                    if (r.sucesso && r.conteudo) {
                        this.record = r.conteudo;

                        this.appUpload.setArquivo(this.record.arquivo!);
                        //this.selectedPerfis = this.usuario.perfis;
                    }
                })
            });
    }

    async getPerfis() {
        this.perfis = [];

        this.loading = true;
        await this.perfisService.getPerfis()
            .then((res: any) => {
                res.subscribe((r: any) => {
                    this.loading = false;
                    if (r && r.sucesso) {
                        this.perfis = r.conteudo;
                    } else {
                        this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', r.mensagemRetorno);
                    }
                })
            });
    }

    async salvar_click() {

        // this.usuario.perfis = this.selectedPerfis;

        this.record.idArquivo = this.appUpload.getIdArquivo()!;

        console.log('Cliente', this.record);

        await this.service.save(this.record)
            .then((res: any) => {
                res.subscribe((r: any) => {
                    if (r && r.sucesso === true) {
                        this.appComponent.showMessage('Registro salvo com sucesso!');
                    }
                    else {
                        this.appComponent.showErrorMessage('ocorreu um problema');
                    }
                });
            });
    }

}  