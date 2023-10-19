import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { User } from '../../app/login/usuarioEntity';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<User>({} as any);
        this.user = this.userSubject.asObservable();
    }

    public get userValue(): User {

        if (localStorage.getItem('userToken') != null) {
            // let userToken = localStorage.getItem('userToken') || '{}';
            let userToken = localStorage.getItem('userToken');
            let jsonObj = JSON.parse(userToken!);
            let us = new User();
            us = jsonObj as User;
            return us;
        }

        return this.userSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, { username, password }, { withCredentials: true })
            .pipe(map(user => {

                if (user.sucesso === false) {
                    this.handleError(user.mensagemRetorno);
                }

                this.userSubject.next(user);
                localStorage.setItem('userToken', JSON.stringify(user));
                localStorage.setItem('refreshToken', user.jwtToken);
                //localStorage.setItem('refreshToken', JSON.stringify(user.jwtToken));

                this.startRefreshTokenTimer();
                return user;
            }), catchError(this.handleError));
    }

    handleError(error: any) {
        //Limpar Token
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('userToken');

        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            // client-side error
            errorMessage = `Error: ${error.error.message}`;
        } else {
            // server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }

        return throwError(() => {
            return errorMessage;
        });
    }

    logout() {
        this.http.post<any>(`${environment.apiUrl}/users/revoke-token`, {}, { withCredentials: true }).subscribe();
        this.stopRefreshTokenTimer();
        this.userSubject.next({} as any);
        this.router.navigate(['/login']);
    }

    obterRefreshTokenCookies() {
        console.log('obterRefreshTokenCookies');

        if (localStorage.getItem('refreshToken')) {
            //Verificar validade token            
            return (localStorage.getItem('refreshToken'));
        }
        else
            return null;
    }

    refreshToken() {

        return this.http.post<any>(`${environment.apiUrl}/users/refresh-token`, {}, { withCredentials: true })
            .pipe(map((user) => {
                this.userSubject.next(user);
                this.startRefreshTokenTimer();
                return user;
            }));
    }

    // helper methods

    private refreshTokenTimeout: any;

    private startRefreshTokenTimer() {
        //    // parse json object from base64 encoded jwt token
        //     const to = atob(this.userValue.jwtToken?.split('.')[1]);

        //     const jwtToken = JSON.parse(to);

        //     // set a timeout to refresh the token a minute before it expires
        //     const expires = new Date(jwtToken.exp * 1000);
        //     const timeout = expires.getTime() - Date.now() - (60 * 1000);
        //     this.refreshTokenTimeout = setTimeout(() => this.refreshToken().subscribe(), timeout);
    }

    private stopRefreshTokenTimer() {
        clearTimeout(this.refreshTokenTimeout);
    }

    setBaseUrlApi() {
        environment.apiUrl = `${environment.protocolo}://${window.location.hostname}:${environment.porta}`;
    }

    getNomeUsuarioLogado() {
        let user = this.userValue;
        if (user)
            return user.username;
        else
            return '';
    }
}