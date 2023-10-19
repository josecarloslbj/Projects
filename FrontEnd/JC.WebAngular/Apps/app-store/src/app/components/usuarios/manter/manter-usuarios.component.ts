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
import { UsuarioModel } from '../model/usuarios-model';
import { UsuarioService } from '../usuario.service';
import { AppComponent } from 'src/app/app.component';
import { MessageService } from 'primeng/api';
import { UploadComponent } from 'src/app/shared/componentes/upload/upload.component';


@Component({
    selector: 'app-manter-usuario',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './manter-usuarios.component.html',
    styleUrls: ['./manter-usuarios.component.scss'],
    providers: [GroupByPipe, MessageService]

})
export class ManterUsuarioComponent implements OnInit, AfterViewInit {

    usuario: UsuarioModel = new UsuarioModel();
    id: number = 0;
    loading: boolean = false;

    perfis: PerfilEntidade[] = [];
    selectedPerfis: PerfilEntidade[] = [];


    @ViewChild('appUpload') appUpload!: UploadComponent;
    
    constructor(
        private usuarioService: UsuarioService,
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

        this.getPerfis();
        this.getUsuario();
    }

    ngAfterViewInit(): void {        
        if (this.id == 0) {
            this.appUpload.setArquivo(undefined);
        }
    }
    ngOnDestroy() { }

    async getUsuario() {
        this.usuario = new UsuarioModel();
        if (this.id == 0) return;

        this.loading = true;
        await this.usuarioService.getUsuario(this.id)
            .then(res => {
                res.subscribe(r => {
                    this.loading = false;
                    if (r.sucesso && r.conteudo) {
                        this.usuario = r.conteudo;
                        this.selectedPerfis = this.usuario.perfis;

                        this.appUpload.setArquivoById(this.usuario.idArquivo!);
                      
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

        this.usuario.perfis = this.selectedPerfis;
        this.usuario.idArquivo = this.appUpload.getIdArquivo()!;

        await this.usuarioService.save(this.usuario)
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