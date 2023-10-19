
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';


@Injectable()
export class BaseService {
    constructor() { }

    get setBaseUrlApi() {

      environment.apiUrl = "localhost:9999";
      return '';
    }
}
