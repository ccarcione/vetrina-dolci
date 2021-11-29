import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isAuthenticated: boolean = false;

  constructor(public oidcSecurityService: OidcSecurityService) { }

  ngOnInit(): void {
    this.oidcSecurityService.isAuthenticated$.subscribe(x => this.isAuthenticated = x.isAuthenticated);
  }

  login() {
    this.oidcSecurityService.authorize();
  }
  
  logout() {
    this.oidcSecurityService.logoff();
    this.oidcSecurityService.revokeAccessToken();
    this.oidcSecurityService.revokeRefreshToken();
  }
}
