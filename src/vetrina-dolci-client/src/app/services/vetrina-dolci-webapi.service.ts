import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: 'root'
})
export class VetrinaDolciWebapiService {
  urlWebAPI = 'https://localhost:6001/';

  httpOptions = {
    headers: new HttpHeaders({
      Authorization: 'Bearer ' + this.oidcSecurityServices.getAccessToken(),
    }),
  };

  constructor(public oidcSecurityServices: OidcSecurityService, private http: HttpClient) { }

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.oidcSecurityServices.getAccessToken(),
      }),
    };
  }

  getWeatherForecast() {
    return this.http.get(this.urlWebAPI + 'WeatherForecast', this.getHttpOptions());
  }
}
