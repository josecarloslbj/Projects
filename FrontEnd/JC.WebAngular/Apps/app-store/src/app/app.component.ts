import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { AuthenticationService } from './login/authentication.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app-store';
  mostrarMenu: boolean = false;


  toastPosition: string = 'top-center';
  toastFontColorTitle: string = '#444';
  toastFontColorMsg: string = '#444';
  toastShowDetailsError: boolean = false;
  toastDetailsError?: string = undefined;

  showDialogDetaisError: boolean = false;


  public display: boolean = false;

  constructor(private authService: AuthenticationService,
    private translateService: TranslateService,
    private config: PrimeNGConfig,
    private messageService: MessageService) {
  }

  ngOnInit() {
    this.authService.setBaseUrlApi();
    this.translateService.setDefaultLang('pt-BR');
    this.translate('pt-BR')
  }

  translate(lang: string) {
    this.translateService.use(lang);
    this.translateService.get('primeng').subscribe(res => this.config.setTranslation(res));
}


  showMessage(detalhe: string) {
    this.toastPosition = 'top-right';
    this.toastFontColorTitle = '#7CB342';
    this.toastFontColorMsg = '#54782e';

    this.toastShowDetailsError = false;
    this.toastDetailsError = undefined;
    this.messageService.add({
      severity: 'success',
      summary: 'Sucesso',
      detail: detalhe
    });
  }

  showErrorMessage(detail: string, _toastDetailsError?: string, summary?: string, toastPosition?: string) {
    this.toastShowDetailsError = _toastDetailsError !== undefined;
    this.toastDetailsError = _toastDetailsError as string;

    this.toastPosition = toastPosition !== undefined ? toastPosition : 'top-center';
    this.toastFontColorTitle = '#EF4444';
    this.toastFontColorMsg = '#EF4444';
    this.messageService.add({
      severity: 'error',
      summary: summary !== undefined ? summary : 'Ops...',
      detail: detail
    });
  }

  visualizarDetalheErro(toastDetailsError: any) {
    this.messageService.clear();
    this.showDialogDetaisError = true;
  }

  public showDialog() {
    this.display = true;
  }
}
