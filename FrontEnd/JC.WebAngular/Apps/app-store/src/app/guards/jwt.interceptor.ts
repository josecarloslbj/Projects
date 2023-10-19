import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthenticationService } from '../login/authentication.service';



@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to the api url
        let user = this.authenticationService.userValue;
        let userLogado = false;

        if (!user) {
            let userToken = localStorage.getItem('userToken') || '{}';
            user = JSON.parse(userToken);
        }
        
        // console.log('intercept', user); 
        const isLoggedIn = user && user.jwtToken;
        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if ((isLoggedIn || userLogado) && isApiUrl) {

            if (user.jwtToken == undefined) {
                let userToken = localStorage.getItem('userToken') || '{}';
                user = JSON.parse(userToken);
            }

            request = request.clone({
                setHeaders: { Authorization: `Bearer ${user.jwtToken}` }
            });
        }

        return next.handle(request);
    }
}