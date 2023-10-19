
import { Router } from '@angular/router';
import { Component, OnInit, AfterViewInit, Injectable } from '@angular/core';
import {
    trigger,
    state,
    style,
    animate,
    transition,
    // ...
} from '@angular/animations';
import { AberturaFechamentoCaixaModel } from '../model/aberturafechamentocaixa.Model';
import { VendaService } from '../venda.service';
import { AppComponent } from 'src/app/app.component';
import { AuthenticationService } from 'src/app/login/authentication.service';
import { SituacaoCaixa } from 'src/app/shared/enumeradores/enum';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';


@Injectable({
    providedIn: 'root'
})
@Component({
    selector: 'app-aberturafechamentocaixa-caixa',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],        // end of trigger block    
    templateUrl: './aberturafechamentocaixa.component.html',
    styleUrls: ['./aberturafechamentocaixa.component.scss'],
})
export class AberturaFechamentoCaixaComponent implements OnInit, AfterViewInit {

    dataAtual = new Date();
    loading: boolean = false;
    record: AberturaFechamentoCaixaModel = new AberturaFechamentoCaixaModel();

    mostrarfecharCaixa: boolean = false;

    constructor(
        private vendaService: VendaService,
        private appComponent: AppComponent,
        private authenticationService: AuthenticationService,
        private confirmationService: ConfirmationService
    ) { }

    ngOnInit() {
        this.getStatusCaixa();
    }

    confirm() {
        this.confirmationService.confirm({
            message: 'Deseja abrir o caixa com o valor inicial de: ' + this.record.valorInicial.toLocaleString('en-US', { style: 'currency', currency: 'BRL' }) + ' ?',
            accept: () => {
                this.abrirCaixa();
            }
        });
    }

    abrirCaixa() {
        this.record.nomeUsuarioAbertura = this.authenticationService.userValue.username!;
        this.record.idUsuarioAbertura = this.authenticationService.userValue.id!;

        this.vendaService.abrirCaixa(this.record)
            .then(res => res.subscribe((r: any) => {
                this.loading = false;
                if (r.sucesso && r.conteudo) {
                    this.record = r.conteudo;
                } else this.showError(r.mensagemRetorno);
            }))
            .catch((erro) => { this.showError(erro); });
    }

    fecharCaixa(){
        this.vendaService.fecharCaixa(this.record)
            .then(res => res.subscribe((r: any) => {
                this.loading = false;
                if (r.sucesso && r.conteudo) {
                    this.record = r.conteudo;
                    this.voltarFecharCaixar();
                } else this.showError(r.mensagemRetorno);
            }))
            .catch((erro) => { this.showError(erro); });
    }

    getStatusCaixa() {
        this.record = new AberturaFechamentoCaixaModel();

        this.loading = true;
        this.vendaService.getStatusCaixa()
            .then(res => res.subscribe((r: any) => {
                this.loading = false;
                if (r.sucesso && r.conteudo) {
                    this.record = r.conteudo;

                    if (this.record.situacao == SituacaoCaixa.AGUARDANDO_ABERTURA)
                        this.record.nomeUsuarioAbertura = this.authenticationService?.userValue?.username!;
                } else this.showError(r.mensagemRetorno);
            }))
            .catch((erro) => { this.showError(erro); });
    }

    showError(msg?: string) {
        this.appComponent.showErrorMessage('Não foi possível recuperar o registro.', msg);
    }

    ngAfterViewInit() {
    }

    ngOnDestroy() {

    }

    disabledFields() {
        if (this.record.situacao == SituacaoCaixa.AGUARDANDO_ABERTURA) return false;
        return true;
    }

    mostrarFooter() {        
        let mostrar = true;
        if (this.record && this.record.situacao == SituacaoCaixa.ABERTO) mostrar = false;
        return mostrar;
    }
    mostrarFecharCaixar() {
        this.record.dataFechamento = new Date();
        this.mostrarfecharCaixa = !this.mostrarfecharCaixa;
    }

    voltarFecharCaixar(){
        this.mostrarfecharCaixa = !this.mostrarfecharCaixa;        
    }

    confirmFecharCaixar() {
        this.confirmationService.confirm({
            message: 'Deseja fechar o caixa com o valor final de: ' + this.record.valorFinal.toLocaleString('en-US', { style: 'currency', currency: 'BRL' }) + ' ?',
            accept: () => {
                this.fecharCaixa();
            }
        });
    }
}

