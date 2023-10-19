import { ActivatedRoute, Router } from '@angular/router';
import { PerfisService } from '../perfis.service';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
  // ...
} from '@angular/animations';
import { CategoriaPermissaoEntidade, PerfilEntidade, PermissaoEntidade } from '../entidade/perfil-entidade';
import { GroupByPipe } from 'src/app/shared/pipes/groupBy';
import { Message, MessageService } from 'primeng/api';
import { first } from 'rxjs';

@Component({
  selector: 'app-manter-perfis',
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0.3 }),
        animate(500, style({ opacity: 1 }))
      ])
    ])
  ],

  templateUrl: './manter-perfis.component.html',
  styleUrls: ['./manter-perfis.component.scss'],
  providers: [GroupByPipe, MessageService]

})
export class ManterPerfisComponent implements OnInit, AfterViewInit {
  msgs: Message[] = [];

  permissoes: CategoriaPermissaoEntidade[] = [];
  perfil: PerfilEntidade = new PerfilEntidade();
  permissoesSelecionadas: PermissaoEntidade[] = [];
  id: number = 0;
  loading: boolean = false;
  products: any[] = [];
  relatorios: any[] = [];
  vendas: any[] = [];

  constructor(
    private perfisService: PerfisService,
    private route: ActivatedRoute,
    private router: Router,
    private messageService: MessageService
  ) { }

  ngOnInit() {

    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (isNaN(Number(this.id))) {
      this.router.navigate(['perfis']);
    }


    this.getPermissoes();
    this.getPerfil();
  }

  ngAfterViewInit(): void {

  }

  ngOnDestroy() {

  }

  async salvar_click() {

    this.perfil.permissoes = this.permissoesSelecionadas;
    this.perfisService.save(this.perfil);

    let salvarPerfil = await this.perfisService.save(this.perfil);
    salvarPerfil.toPromise()
      .then((res: any) => {
        if (res !== undefined) {
          if (res.sucesso === true) {
            this.messageService.add({ severity: 'success', summary: 'Service Message', detail: 'Via MessageService' });
          }
          else {
            this.messageService.add({ severity: 'error', summary: 'Ocorreu um erro', detail: res.excecao });
          }
        } else {
          this.messageService.add({ severity: 'error', summary: 'Ocorreu um erro', detail: 'Erro dewconhecido' });
        }
      })
      .catch((error) => { console.log('catch', error); })
  }

  async getPermissoes() {
    this.permissoes = [];

    this.loading = true;
    await this.perfisService.getPermissoes()
      .then(res => {
        res.subscribe(r => {
          this.permissoes = r.conteudo;
        })
      })
      .finally(() => {
        this.loading = false;
      });
  }

  async getPerfil() {
    this.perfil = new PerfilEntidade();

    if (this.id == 0) {     
      return;
  }

    this.loading = true;
    await this.perfisService.getPerfil(this.id)
      .then(res => {
        res.subscribe(r => {
          if (r.sucesso && r.conteudo) {
              this.perfil = r.conteudo;
              this.permissoesSelecionadas = this.perfil.permissoes;
          }
        })
      })
      .finally(() => {
        this.loading = false;
      });
  }

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

