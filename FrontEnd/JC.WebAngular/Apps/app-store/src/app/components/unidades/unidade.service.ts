import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { EntityFilters, FieldsFilter, PagedInputDTO } from '../../shared/paginator/pagedInputDTO';

@Injectable()
export class UnidadeService {

  endpoint: string = 'unidade';

  constructor(
    private http: HttpClient) { }

  async save(entity: any) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/salvar`, entity);
  }

  async get(id: number) {
    return await this.http.get<any>(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }

  async getPaged(event: PagedInputDTO) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/buscar`, event);
  }

  async delete(id: number) {
    return await this.http.delete(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }
}
