
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
  HttpHandler,
  HttpEvent,
  HttpResponse
} from '@angular/common/http';

import { Observable, EMPTY, throwError, of } from 'rxjs';
import { catchError, } from 'rxjs/operators';
import { MessageService } from 'primeng/api';
import { AppComponent } from '../app.component';

@Injectable({ providedIn: 'root' })
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private messageService: MessageService,
    private appComponent: AppComponent) {  
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.error instanceof Error) {
          // A client-side or network error occurred. Handle it accordingly.
          console.log('An error occurred:', error.error.message);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong,
          

          if (error.error.message === 'Username or password is incorrect') {
            this.messageService.add({
              key: 'notification',
              severity: 'error',
              summary: `Login e/ou senha incorreto!`,
            });

            return throwError(error);
          }
          else{
            this.messageService.add({
              key: 'notification',
              severity: 'error',
              summary: `Ops! Tente novamente mais tarde! Erro: ${error.error.message}`,
            });
          }

          return of(new HttpResponse({
            body: {
              sucesso: false,
              mensagemRetorno: `Backend returned code ${error.status}, body was: ${error.error.message}`
            }
          }));

        }

        // If you want to return a new response:
        //return of(new HttpResponse({body: [{name: "Default value..."}]}));

        // If you want to return the error on the upper level:
        //return throwError(error);

        // or just return nothing:
        //return EMPTY;

        return EMPTY;

      })
    );
  }


}