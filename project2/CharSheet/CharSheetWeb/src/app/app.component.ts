import { Component, Injectable, Inject } from '@angular/core';
import { AuthService, GoogleLoginProvider, SocialUser } from "angularx-social-login";
import { ApiService } from './api.service';
import { CookieService } from 'ngx-cookie-service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'Character Sheet Creator';
  user: any;

  constructor(private _socioAuthServ: AuthService, private apiService: ApiService, private cookieService: CookieService) { }

  // Method to sign in with google.
  singIn(platform: string): void {
    platform = GoogleLoginProvider.PROVIDER_ID;
    this._socioAuthServ.signIn(platform).then(
      (response) => {
        console.log(platform + " logged in user data is= ", response);
        this.user = response;
        return response;
      }
    ).then(response => {
      this.apiService.googleLogin(response)
        .subscribe((response) => {
          if (response.status == 200) {
            console.log(response);
            this.cookieService.set('access_token', response.body);
          }
        });
    });
  }

  // Method to log out.
  signOut(): void {
    this._socioAuthServ.signOut();
    this.user = null;
    console.log('User signed out.');
  }
}
