import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { PaginationResult } from '../models/pagination-result';
import { Dolce } from '../models/dolce';
import { Pagination } from '../models/pagination';
import { DolceInVendita } from '../models/dolce-in-vendita';

@Injectable({
  providedIn: 'root'
})
export class VetrinaDolciWebapiService {
  urlDolciAPI = 'api/Dolci/';
  urlDolciInVenditaAPI = 'api/DolciInVendita/';

  constructor(public oidcSecurityServices: OidcSecurityService, private http: HttpClient) { }

  private getHttpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.oidcSecurityServices.getAccessToken(),
      }),
    };
  }

  getWeatherForecast() {
    return this.http.get(`api/WeatherForecast`, this.getHttpOptions());
  }

  getAllDolci(paginazione: Pagination) {
    return this.http.post<PaginationResult<Dolce>>(
      `${this.urlDolciAPI}GetPaginazione/`,
      {
        params:
        {
          page: paginazione.pageIndex.toString(),
          pageCount: paginazione.pageSize.toString()
        }
      },
      this.getHttpOptions());
  }

  getDolce(id: number) {
    return this.http.get(`${this.urlDolciAPI}${id}`, this.getHttpOptions());
  }
  
  getDolceInVendita(id: number) {
    return this.http.get(`${this.urlDolciInVenditaAPI}${id}`, this.getHttpOptions());
  }

  async getAllDolciInVendita(paginazione: Pagination): Promise<PaginationResult<DolceInVendita>>{
    return await this.http.post<PaginationResult<DolceInVendita>>(
      `${this.urlDolciInVenditaAPI}GetPaginazione`,
      {
        params:
        {
          page: paginazione.pageIndex.toString(),
          pageCount: paginazione.pageSize.toString()
        }
      },
      this.getHttpOptions())
      .toPromise();
  }

  putDolceInVendita(dolceInVendita: DolceInVendita) {
    return this.http.put<DolceInVendita>(this.urlDolciInVenditaAPI, dolceInVendita, this.getHttpOptions());
  }
  
  postDolceInVendita(dolceInVendita: DolceInVendita) {
    return this.http.post<DolceInVendita>(this.urlDolciInVenditaAPI, dolceInVendita, this.getHttpOptions());
  }

  deleteDolceInVendita(id: number) {
    return this.http.delete(`${this.urlDolciInVenditaAPI}/${id}`, this.getHttpOptions());
  }
}
