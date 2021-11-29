import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Pagination } from './models/pagination';
import { HelperService } from './services/helper.service';
import { VetrinaDolciWebapiService } from './services/vetrina-dolci-webapi.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'vetrina-dolci-client';
  imgUrl: string = '';

  constructor(public oidcSecurityService: OidcSecurityService,
    private vetrinaDolciWebapiService: VetrinaDolciWebapiService,
    private helperService: HelperService) {}

  ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated, userData, accessToken, idToken }) => {
      console.log('isAuthenticated:', isAuthenticated)
    });
  }

  async callApi() {
    await this.vetrinaDolciWebapiService.getWeatherForecast().subscribe(s => console.log(s));
  }
  
  async searchImage() {
    await this.helperService.getImageFromPixabay('caffe').subscribe((s: any) => {
      if (s.total == '0') {
        // load default image
        this.imgUrl = 'assets/image-not-found.png';
      } else {
        this.imgUrl = s.hits[0]?.webformatURL;
      }
    });
  }
  
  testGetDolce(id: number) {
    this.vetrinaDolciWebapiService.getDolce(id).subscribe(x => console.log(x));
  }

  testGetAllDolci() {
    let p = new Pagination();
    p.pageIndex = 1;
    // p.pageSize = 10;
    
    this.vetrinaDolciWebapiService.getAllDolci(p).subscribe(x => console.log(x));
  }
}
