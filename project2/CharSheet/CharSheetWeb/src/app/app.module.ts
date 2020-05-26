import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// Importing social login module and google login provider.
import { SocialLoginModule, AuthServiceConfig, GoogleLoginProvider } from "angularx-social-login";

import { AppComponent } from './app.component';

// Client id for the google oauth. This is used for validation of our application to google.
const google_oauth_client_id: string = '988638575381-34kpeeoin969ru3r25st68ven9gbi9kg.apps.googleusercontent.com';
let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider(google_oauth_client_id)
  }
]);

@NgModule({
  declarations: [
    AppComponent
  ],
  // Injecting the social-login-module during the application startup!
  imports: [
    BrowserModule, SocialLoginModule.initialize(config)
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
