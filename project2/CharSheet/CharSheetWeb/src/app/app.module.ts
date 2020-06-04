import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
// Importing social login module and google login provider.
import { SocialLoginModule, AuthServiceConfig, GoogleLoginProvider, AuthService } from "angularx-social-login";
import { AppComponent } from './app.component';
import { CreateFormComponent } from './create-form/create-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { ResizableModule } from 'angular-resizable-element';
import { AccessCharSheetsComponent } from './access-char-sheets/access-char-sheets.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsContainerComponent } from './forms-container/forms-container.component';
import { AppRoutingModule } from './app-routing.module';
import { ApiService } from './api.service';
import { SelectedCharSheetComponent } from './selected-char-sheet/selected-char-sheet.component';
import { SheetsComponent } from './sheets/sheets.component';



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
    AppComponent,
    CreateFormComponent,
    AccessCharSheetsComponent,
    FormsContainerComponent,

    SelectedCharSheetComponent


    SheetsComponent

  ],
  // Injecting the social-login-module during the application startup!
  imports: [
    BrowserModule,
    SocialLoginModule.initialize(config),
    BrowserAnimationsModule,
    DragDropModule,
    ResizableModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    CookieService,
    ApiService
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
