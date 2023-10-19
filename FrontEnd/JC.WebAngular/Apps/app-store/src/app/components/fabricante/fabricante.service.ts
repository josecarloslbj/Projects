import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { FabricanteModel } from './model/fabricante-model';

@Injectable()
export class FabricanteService {

  endpoint: string = 'fabricante';

  constructor(
    private http: HttpClient) { }

  async save(entity: FabricanteModel) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/salvar`, entity);
  }

  async get(id: number) {
    return await this.http.get<any>(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }

  async getPaged(event: FabricanteModel) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/buscar-fabricantes`, event);
  }

  async delete(id: number) {
    return await this.http.delete(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }
}
