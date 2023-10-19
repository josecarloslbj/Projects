import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { FabricanteModel } from '../fabricante/model/fabricante-model';
import { FornecedorModel } from '../fornecedor/model/fornecedor-model';
import { GrupoProdutoModel, ProdutoModel } from './model/produto-model';
import { firstValueFrom, map } from 'rxjs';


@Injectable()
export class ProdutoService {

  endpoint: string = 'produto';

  constructor(
    private http: HttpClient) { }

  async save(entity: ProdutoModel) {
    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/salvar`, entity);
  }

  async get(id: number) {
    return await this.http.get<any>(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }

  async getPaged(event: ProdutoModel) {

    let table: any = JSON.parse(JSON.stringify(event))

    if (table?.filters?.ativo?.value === true)
      table.filters.ativo.value = 1;
    else if (table?.filters?.ativo?.value === false)
      table.filters.ativo.value = 0;

    return await this.http.post(`${environment.apiUrl}/${this.endpoint}/buscar-produtos`, table);
  }

  async delete(id: number) {
    return await this.http.delete(`${environment.apiUrl}/${this.endpoint}/${id}`);
  }
  async getByCodigoBarras(codigo: any) {
    return await this.http.get(`${environment.apiUrl}/${this.endpoint}/buscar-por-codigo-barra/${codigo}`);
  }

  async getGrupoProdutoPaged(event?: GrupoProdutoModel) {
    if (!event) event = new GrupoProdutoModel();
    return await this.http.post(`${environment.apiUrl}/grupoproduto/buscar-gruposprodutos`, event)
  }

  async getFornecedorPaged(event?: FornecedorModel) {
    if (!event) event = new FornecedorModel();
    return await this.http.post(`${environment.apiUrl}/fornecedor/buscar-fornecedores`, event);
  }

  async getFabricantePaged(event?: FabricanteModel) {
    if (!event) event = new FabricanteModel();
    return await this.http.post(`${environment.apiUrl}/fabricante/buscar-fabricantes`, event);
  }
}
