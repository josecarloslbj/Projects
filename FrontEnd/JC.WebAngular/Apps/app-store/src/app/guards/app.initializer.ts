import { AuthenticationService } from "../login/authentication.service";

export function appInitializer(authenticationService: AuthenticationService) {

    console.log('appInitializer');

    return () => new Promise((resolve, reject) =>  {
        // attempt to refresh token on app start up to auto authenticate
        // authenticationService.refreshToken()
        //     .subscribe()
        //     .add(resolve);
        console.log('appInitializer',authenticationService.obterRefreshTokenCookies());

            resolve( authenticationService.obterRefreshTokenCookies());
    });
}