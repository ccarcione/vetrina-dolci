import { NgModule } from '@angular/core';
import { AuthModule } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';


@NgModule({
    imports: [AuthModule.forRoot({
        config: {
            authority: environment.authority,
            redirectUrl: window.location.origin,
            postLogoutRedirectUri: window.location.origin,
            clientId: 'vetrina-dolci-client',
            scope: 'openid vetrinadolci.webapi', // 'openid profile ' + your scopes
            responseType: 'code',
            silentRenew: true,
            silentRenewUrl: window.location.origin + '/silent-renew.html',
            renewTimeBeforeTokenExpiresInSeconds: 10
        }
      })],
    exports: [AuthModule],
})
export class AuthConfigModule {}
