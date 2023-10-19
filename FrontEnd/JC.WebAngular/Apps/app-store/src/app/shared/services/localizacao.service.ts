import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { PagedInputDTO } from '../../shared/paginator/pagedInputDTO';

@Injectable()
export class LocalizacaoService {
    constructor(private http: HttpClient) { }

    endPointPais: string = 'pais';
    endPointEstado: string = 'estado';
    endPointCidade: string = 'cidade';
    endPointBairro: string = 'bairro';

    async getPaisPaged(event?: PagedInputDTO) {
        if (event == null) event = new PagedInputDTO();
        return await this.http.post(`${environment.apiUrl}/${this.endPointPais}/buscar-paises`, event);
    }

    async getEstadosPaged(event?: PagedInputDTO) {
        if (event == null) event = new PagedInputDTO();
        return await this.http.post(`${environment.apiUrl}/${this.endPointPais}/buscar-estados`, event);
    }

    async getCidadesPaged(event?: PagedInputDTO) {
        if (event == null) event = new PagedInputDTO();
        return await this.http.post(`${environment.apiUrl}/${this.endPointPais}/buscar-cidades`, event);
    }

    async getBairro(id: number) {
        return await this.http.get<any>(`${environment.apiUrl}/${this.endPointPais}/bairro/${id}`);
    }

    async saveBairro(entity: any) {
        return await this.http.post(`${environment.apiUrl}/${this.endPointPais}/bairro/salvar`, entity);
    }
    async getBairroPaged(event?: PagedInputDTO) {
        if (event == null) event = new PagedInputDTO();
        return await this.http.post(`${environment.apiUrl}/${this.endPointPais}/bairro/buscar`, event);
    }
}
