import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { Component, OnInit, AfterViewInit, ViewEncapsulation, EventEmitter } from '@angular/core';
import {
    trigger,
    state,
    style,
    animate,
    transition,
    // ...
} from '@angular/animations';
import { ConfirmationService } from 'primeng/api';
import { ArquivoModel } from 'src/app/components/produto/model/produto-model';
import { UploadService } from './upload.service';


@Component({
    selector: 'app-upload',
    animations: [
        trigger('fade', [
            transition('void => *', [
                style({ opacity: 0.3 }),
                animate(500, style({ opacity: 1 }))
            ])
        ])
    ],

    templateUrl: './upload.component.html',
    styleUrls: ['./upload.component.scss'],
    //encapsulation: ViewEncapsulation.None,
    //  providers: [MessageService,ConfirmationService]

})
export class UploadComponent implements OnInit, AfterViewInit {

    uploadedFiles: any[] = [];
    baseApi = environment.apiUrl;
    baseImg = environment.production ? environment.apiUrl : environment.imgUrl;
    record: ArquivoModel = new ArquivoModel();
    idArquivo?: number | null;

    uploadEvent?: EventEmitter<any>;

    constructor(
        private confirmationService: ConfirmationService,
        private uploadService: UploadService
    ) { }

    ngAfterViewInit(): void { }

    ngOnInit(): void { }

    onUpload(event: any) {
        this.uploadedFiles = [];
        for (const file of event.files) {
            this.uploadedFiles.push(file);
        }
        if (event && event.originalEvent
            && event.originalEvent.body && event.originalEvent.body.conteudo) {
            this.idArquivo = event.originalEvent.body.conteudo.id;
            this.record.id = this.idArquivo!;
            //this.record.urlArquivo = 'Comuns/Images/' + event.originalEvent.body.conteudo.urlArquivo
            this.record.diretorio = event.originalEvent.body.conteudo.diretorio;
        }
    }

    onSelect(event: any) {
        // this.uploadedFiles = [];
    }
    onRemove(event: any) {
        // this.uploadedFiles = [];
        // this.idArquivo = 0;
        // this.record.idArquivo = this.idArquivo;
    }

    getIdArquivo() {
        return this.idArquivo;
    }

    setArquivo(arquivo?: ArquivoModel) {
        if (!arquivo) {
            this.idArquivo = null;
            this.record = new ArquivoModel();
            this.record.diretorio = 'Comuns/Images/sem-foto.png';
            return;
        };

        this.idArquivo = arquivo!.id;
        this.record = arquivo!;
    }

    setArquivoById(idArquivo: number) {

        if (!idArquivo || idArquivo == 0) {
            this.setArquivo(undefined);
            return;
        }

        this.uploadService.getArquivo(idArquivo)
            .then(res => res.subscribe(r => {
                if (r.sucesso && r.conteudo) {
                    this.record = r.conteudo;
                    this.setArquivo(this.record);
                }
            }));
    };

    deleteConfirm(event: Event, record: ArquivoModel) {

        this.delete(record);

        this.confirmationService.confirm({
            target: event.target as EventTarget,
            header: 'Confirmação',
            message: 'Deseja remover a imagem?',
            icon: 'pi pi-exclamation-triangle',
            rejectLabel: 'não',
            rejectIcon: 'pi pi-times',
            acceptLabel: 'sim',
            acceptIcon: 'pi pi-check',
            defaultFocus: 'reject',
            acceptButtonStyleClass: 'p-button-danger',
            rejectButtonStyleClass: 'p-button-primary',
            accept: () => {
                //confirm action
                this.delete(record);
            },
            reject: () => {
                //reject action
            }
        });
    }


    delete(arquivo: ArquivoModel) {
        arquivo.id = 0;
        this.idArquivo = null;
        this.record = new ArquivoModel();
        //this.record.diretorio = 'Comuns/Images/produto-sem-img.png';
        this.record.diretorio = 'Comuns/Images/sem-foto.png'
    }
}