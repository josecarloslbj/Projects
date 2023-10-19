import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { GrupoProdutoModel } from './model/grupo-produto-model';

@Injectable()
export class GrupoProdutoService {

  endpoint: string = 'grupoproduto';

  constructor(
    private http: HttpClient) { }

  async save(entity: GrupoProdutoModel) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/salvar`, entity);
  }

  async get(id: number) {
    return await this.http.get<any>(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }

  async getPaged(event: GrupoProdutoModel) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/buscar-gruposprodutos`, event);
  }

  async delete(id: number) {
    return await this.http.delete(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }
}
