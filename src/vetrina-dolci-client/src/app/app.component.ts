import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { VetrinaDolciWebapiService } from './services/vetrina-dolci-webapi.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'vetrina-dolci-client';

  constructor(public oidcSecurityService: OidcSecurityService, private vetrinaDolciWebapiService: VetrinaDolciWebapiService) {}

  ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated, userData, accessToken, idToken }) => {
      console.log('isAuthenticated:', isAuthenticated)
    });
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  async callApi() {
    await this.vetrinaDolciWebapiService.getWeatherForecast().subscribe(s => console.log(s));
  }

  logout() {
    this.oidcSecurityService.logoff();
    this.oidcSecurityService.revokeAccessToken();
    this.oidcSecurityService.revokeRefreshToken();
  }
}
