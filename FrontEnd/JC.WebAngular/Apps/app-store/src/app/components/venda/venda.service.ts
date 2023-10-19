import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ProdutoVendaModel } from '../produto/model/produto-model';
import { AberturaFechamentoCaixaModel } from './model/aberturafechamentocaixa.Model';
import { PedidoModel } from './model/pedido.model';


@Injectable()
export class VendaService {

  endpoint: string = 'venda';
  endpointPedido: string = 'pedido';
  endpointCaixa: string = 'caixa';
  baseUrlPedido = `${environment.apiUrl}/${this.endpointPedido}`;
  baseUrlCaixa = `${environment.apiUrl}/${this.endpointCaixa}`;

  constructor(
    private http: HttpClient) { }

  async registrarvenda(entity: any[]) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/registar-venda`, entity);
  }

  async criarPedido(entity: any) {
    return await this.http.post(`${this.baseUrlPedido}/criar-pedido`, entity);
  }

  async getPedidoPaged(event: PedidoModel) {
    return await this.http.post(`${this.baseUrlPedido}/buscar-pedidos`, event);
  }

  async getStatusCaixa() {
    return await this.http.get(`${this.baseUrlCaixa}/obterCaixa`);
  }

  async abrirCaixa(entity: AberturaFechamentoCaixaModel) {
    return await this.http.post(`${this.baseUrlCaixa}/abrirCaixa`, entity);
  }
  
  async fecharCaixa(entity: AberturaFechamentoCaixaModel) {
    return await this.http.post(`${this.baseUrlCaixa}/fecharCaixa`, entity);
  }
}
