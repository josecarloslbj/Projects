import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LazyLoadEvent } from 'primeng/api';
import { map } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { EntityFilters, FieldsFilter, PagedInputDTO } from '../../../shared/paginator/pagedInputDTO';

@Injectable()
export class PaisService {
  constructor(
    private router: Router,
    private http: HttpClient) { }

  async getPerfilsPaged(event: PagedInputDTO) {

    var filters: any = event.filters;
    event.fieldsFilters = [];

    if (filters !== undefined) {
      if (filters.id) {
        let d = new EntityFilters();
        d.fieldName = 'id';

        let ff = new FieldsFilter();
        ff.matchMode = filters.id.value;
        ff.value = filters.id.matchMode;
        d.fields = ff;

        event.fieldsFilters?.push(d);
      }
      if (filters.nome) {
        let d = new EntityFilters();
        d.fieldName = 'nome';

        let ff = new FieldsFilter();
        ff.matchMode = filters.nome.value;
        ff.value = filters.nome.matchMode;
        d.fields = ff;

        event.fieldsFilters?.push(d);
      }

      if (filters.descricao) {
        let d = new EntityFilters();
        d.fieldName = 'descricao';

        let ff = new FieldsFilter();
        ff.matchMode = filters.descricao.value;
        ff.value = filters.descricao.matchMode;
        d.fields = ff;

        event.fieldsFilters?.push(d);
      }
    }

    let url = `${environment.apiUrl}/perfil/buscar-perfis?`;
    if (event != null) {
      if (event.first !== undefined)
        url += `First=${event.first}&`;
      if (event.last !== undefined)
        url += `Last=${event.last}&`;
      if (event.rows !== undefined)
        url += `Rows=${event.rows}&`;
      if (event.sortField !== undefined)
        url += `SortField=${event.sortField}&`;
      if (event.sortOrder !== undefined)
        url += `SortOrder=${event.sortOrder}&`;
    }
    return await this.http.post(`${environment.apiUrl}/perfil/buscar-perfis`, event);
  }

  async savePais(entity: any) {
    return await this.http.post(`${environment.apiUrl}/pais/salvar`, entity);
  }

  async getPais(id: number) {
    return await this.http.get<any>(`${environment.apiUrl}/pais/${id}`);
  }


  async save(entity: any) {
    return await this.http.post(`${environment.apiUrl}/perfil/salvar`, entity);
  }

  async getPermissoes() {
    return this.http.get<any>(`${environment.apiUrl}/perfil/permissoes`);
  }

  async getPerfil(id: number) {
    return await this.http.get<any>(`${environment.apiUrl}/perfil/${id}`);
  }

  async delete(id: number) {
    return await this.http.delete(`${environment.apiUrl}/perfil/${id}`);
  }

  async getPerfis() {
    return await this.http.get<any>(`${environment.apiUrl}/perfil/getPerfis`);
  }

}
