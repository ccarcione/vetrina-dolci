import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: 'root'
})
export class VetrinaDolciWebapiService {
  urlWebAPI = 'https://localhost:6001/';
  token = this.oidcSecurityServices.getAccessToken();

  httpOptions = {
    headers: new HttpHeaders({
      Authorization: 'Bearer ' + this.token,
    }),
  };

  constructor(public oidcSecurityServices: OidcSecurityService, private http: HttpClient) { }

  getWeatherForecast() {
    return this.http.get(this.urlWebAPI + 'WeatherForecast', {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token,
      }),
    });
  }
}
