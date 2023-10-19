import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
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
import { GrupoProdutoModel, ProdutoModel } from '../model/produto-model';
import { ProdutoService } from '../produto.service';
import { FornecedorModel } from '../../fornecedor/model/fornecedor-model';
import { FabricanteModel } from '../../fabricante/model/fabricante-model';

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

    templateUrl: './manter-produto.component.html',
    styleUrls: ['./manter-produto.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [MessageService]

})
export class ManterProdutoComponent extends ManterComumComponent implements OnInit, AfterViewInit {
    tituloPagina: string = 'CADASTRAR PRODUTO';
    urlBase: string = 'produto';
    baseApi = environment.apiUrl;
    baseImg = environment.production ? environment.apiUrl : environment.imgUrl;


    record: ProdutoModel = new ProdutoModel();
    id: number = 0;
    loading: boolean = false;

    idArquivo?: number = undefined;

    uploadedFiles: any[] = [];


    grupos: GrupoProdutoModel[] = [];
    selectedGrupo!: GrupoProdutoModel;

    fornecedores: FornecedorModel[] = [];
    selectedFornecedor!: FornecedorModel;

    fabricantes: FabricanteModel[] = [];
    selectedFabricante!: FabricanteModel;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private produtoService: ProdutoService,
        private appComponent: AppComponent
    ) {
        super();
    }

    override ngOnInit() {

        this.id = Number(this.route.snapshot.paramMap.get('id'));
        if (isNaN(Number(this.id))) {
            this.router.navigate([`${this.urlBase}`]);
        }
        this.carregarDados();

    }

    override ngAfterViewInit(): void {


    }

    override ngOnDestroy() { }

    getRecord() {
        this.record = new ProdutoModel();
        if (this.id == 0) {
            this.record.unidadeMedida = "UN";
            this.record.ativo = true;
            return;
        }

        this.loading = true;
        this.produtoService.get(this.id)
            .then(res => res.subscribe(r => {
                this.loading = false;
                if (r.sucesso && r.conteudo) {
                    this.record = r.conteudo;
                    this.setValueComboBoxRecord();

                    if (this.record.arquivo) {
                        this.uploadedFiles.push(this.record.arquivo);
                    }
                    return;
                }

                this.showError(r.mensagemRetorno);
            }))
            .catch((erro) => { this.showError(erro); });
    }

    setValueComboBoxRecord() {
        if (!this.record || this.record.id == 0) return;
        if (this.grupos && this.record.idGrupo && this.record.idGrupo > 0) {
            var grupo = this.grupos.find(q => q.id == this.record.idGrupo);
            if (grupo) this.selectedGrupo = grupo;
        }

        if (this.fabricantes && this.record.idFabricante && this.record.idFabricante > 0) {
            var fabricante = this.fabricantes.find(q => q.id == this.record.idFabricante);
            if (fabricante) this.selectedFabricante = fabricante;
        }

        if (this.fornecedores && this.record.idFornecedor && this.record.idFornecedor > 0) {
            var fornecedor = this.fornecedores.find(q => q.id == this.record.idFornecedor);
            if (fornecedor) this.selectedFornecedor = fornecedor;
        }
    }
    async salvar_click() {

        this.loading = true;

        if (this.selectedGrupo)
            this.record.idGrupo = this.selectedGrupo.id;

        if (this.selectedFabricante)
            this.record.idFabricante = this.selectedFabricante.id;

        if (this.selectedFornecedor)
            this.record.idFornecedor = this.selectedFornecedor.id;

        this.produtoService.save(this.record)
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

    async voltar_click() {
        this.router.navigate([`${this.urlBase}/`]);
    }

    onUpload(event: any) {
        this.uploadedFiles = [];
        for (const file of event.files) {
            this.uploadedFiles.push(file);
        }
        if (event && event.originalEvent
            && event.originalEvent.body && event.originalEvent.body.conteudo) {
            this.idArquivo = event.originalEvent.body.conteudo.id;
            this.record.idArquivo = this.idArquivo;
            this.record.urlArquivo = 'Comuns/Images/' + event.originalEvent.body.conteudo.urlArquivo
        }
    }

    onSelect(event: any) {
        this.uploadedFiles = [];
    }
    onRemove(event: any) {
        this.uploadedFiles = [];
        this.idArquivo = 0;
        this.record.idArquivo = this.idArquivo;
    }

    carregarDados() {

        this.grupos = [];
        this.fornecedores = [];
        this.fabricantes = [];

        this.produtoService.getGrupoProdutoPaged()
            .then(res => res.subscribe((r: any) => {
                if (r.sucesso) {
                    this.grupos = r.conteudo.items;
                    this.carregarFornecedores();
                    return;
                }
                this.showError(r.mensagemRetorno);
            }))
            .catch((erro) => { this.showError(erro); })
    }

    showError(msg?: string) {
        this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', msg);
    }

    carregarFornecedores() {
        this.produtoService.getFornecedorPaged()
            .then(res => res.subscribe((r: any) => {
                if (r.sucesso) {
                    this.fornecedores = r.conteudo.items;
                    this.carregarFabricantes();
                    return;
                }
                this.showError(r.mensagemRetorno);
            }))
            .catch((erro) => { this.showError(erro); })
    }

    carregarFabricantes() {
        this.produtoService.getFabricantePaged()
            .then(res => res.subscribe((r: any) => {
                if (r.sucesso) {
                    this.fabricantes = r.conteudo.items;

                    this.getRecord();
                    return;
                }
                this.showError(r.mensagemRetorno);
            }))
            .catch((erro) => { this.showError(erro); })
    }
}  