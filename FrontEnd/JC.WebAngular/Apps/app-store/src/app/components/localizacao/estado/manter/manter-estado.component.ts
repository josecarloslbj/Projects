
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
  // ...
} from '@angular/animations';
import { Message, MessageService, SelectItem } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { AppComponent } from 'src/app/app.component';
import { LocalizacaoService } from 'src/app/shared/services/localizacao.service';
import { Table } from 'primeng/table';
import { GroupByPipe } from 'src/app/shared/pipes/groupBy';
import { EstadoModel } from '../model/estado.model';

@Component({
  selector: 'app-manter-estado',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],

  templateUrl: './manter-estado.component.html',
  styleUrls: ['./manter-estado.component.scss'],
  providers: [GroupByPipe, MessageService]

})
export class ManterEstadoComponent implements OnInit, AfterViewInit {
  msgs: Message[] = [];  
  record: EstadoModel = new EstadoModel();
  
  id: number = 0;
  loading: boolean = false;
  products: any[] = [];
  relatorios: any[] = [];
  vendas: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private messageService: MessageService
  ) { }

  ngOnInit() {

    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (isNaN(Number(this.id))) {
      this.router.navigate(['estado']);
    }
  }

  ngAfterViewInit(): void {  }

  ngOnDestroy() {  }

  async salvar_click() {  }

  async getPermissoes() { }
  
  proximaPagina() {
    //this.pagina++;
  }

  pesquisar() {
    this.loading = true;
    setTimeout(() => {
      this.loading = false;
    }, 2200);
  }

  clickMethod(item: any, event: any) {
    console.log('item', item);
    console.log('event', event);
  }
  getValueCheck(item: any) {
    console.log('getValueCheck', item);
  }

}

