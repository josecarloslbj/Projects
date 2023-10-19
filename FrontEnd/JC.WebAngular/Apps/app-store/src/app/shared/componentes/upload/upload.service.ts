
import { Injectable } from "@angular/core";
import { ParamUpload } from "./param-upload";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";

@Injectable()
export class UploadService {

    endpoint: string = 'upload';

    constructor(
        private http: HttpClient) { }

    async getArquivo(id: number) {
        return await this.http.get<any>(`${environment.apiUrl}/${this.endpoint}/${id}`);
    }
}
