import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { PagedInputDTO } from '../../shared/paginator/pagedInputDTO';
import { ClienteModel } from './model/cliente.model';

@Injectable()
export class ClienteService {

  endpoint: string = 'cliente';

  constructor(
    private http: HttpClient) { }

  async save(entity: any) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/salvar`, entity);
  }

  async getUsuario(id: number) {
    return await this.http.get<any>(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }

  async getUsuarioPaged(event: PagedInputDTO) {

    let table: any = JSON.parse(JSON.stringify(event))

    if (table?.filters?.ativo?.value === true)
      table.filters.ativo.value = 1;
    else if (table?.filters?.ativo?.value === false)
      table.filters.ativo.value = 0;

    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/buscar-clientes`, table);
  }

  async getClientes(event: ClienteModel) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/buscar-clientes`, event);
  }

  async delete(id: number) {
    return await this.http.delete(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }
}
