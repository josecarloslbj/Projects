import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { PagedInputDTO } from '../../shared/paginator/pagedInputDTO';
import { FornecedorModel } from './model/fornecedor-model';

@Injectable()
export class FornecedorService {

  endpoint: string = 'fornecedor';

  constructor(
    private http: HttpClient) { }

  async save(entity: FornecedorModel) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/salvar`, entity);
  }

  async get(id: number) {
    return await this.http.get<any>(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }

  async getPaged(event: FornecedorModel) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/buscar-fornecedores`, event);
  }

  async delete(id: number) {
    return await this.http.delete(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }
}
