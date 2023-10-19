
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanLoad, Route } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from '../login/authentication.service';
import jwt_decode from "jwt-decode";


@Injectable()
export class AuthGuard implements CanActivate, CanLoad {

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {

    console.log('AuthGuard => canActivate');

    return this.verificarAcesso();
  }

  private verificarAcesso() {
    // if (this.authService.usuarioEstaAutenticado()){
    //   return true;
    // } 

    //Instalar JWT do Front conferir se o TOKEN ainda é VALIDO 
    if (localStorage.getItem('refreshToken')) {
      const token = localStorage.getItem('refreshToken')!.toString();
      const tokenInfo = this.getDecodedAccessToken(token); // decode token
      if (tokenInfo == null) {
        this.router.navigate(['/login']);
        return false;
      }

      const expireDate = tokenInfo.exp; // get token expiration dateTime

      const expires = new Date(expireDate * 1000);
      const timeout = expires.getTime() - Date.now();     

      if (expires < new Date()) {
        this.router.navigate(['/login']);
        return false;
      }

      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }

  canLoad(route: Route): Observable<boolean> | Promise<boolean> | boolean {
    console.log('canLoad: verificando se usuário pode carregar o cod módulo');

    return this.verificarAcesso();
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch (Error) {
      return null;
    }
  }

}
