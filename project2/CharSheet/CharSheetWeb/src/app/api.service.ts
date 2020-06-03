import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { SocialUser } from 'angularx-social-login';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})

export class ApiService {
  constructor(private httpClient: HttpClient, private cookieService: CookieService) { }

  private connectionString = 'https://revatureprojectapi.azurewebsites.net/api/';
  // private connectionString = 'http://localhost:5000/';

  public userLogin(login: Login): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.connectionString + 'account/login', login, { headers, responseType: 'text', observe: 'response' });
  }

  public googleLogin(user: SocialUser): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.connectionString + 'account/googlelogin', { username: user.name, email: user.email }, { headers, responseType: 'text', observe: 'response' });
  }

  public register(register: Register): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.connectionString + 'account/register', register, { headers, observe: 'response' });
  }

  public getTemplate(templateId: string): Observable<any> {
    return this.httpClient.get(this.connectionString + `templates/${templateId}`, { observe: 'response' })
  }

  public postTemplate(template: Template): Observable<any> {
    const headers = new HttpHeaders()
    .set('Content-type', 'application/json')
    .set('Authorization', `Bearer ${this.cookieService.get('apiToken')}`)
    .set('Authenticationtoken', `Bearer ${this.cookieService.get('apiToken')}`)
    .set('X-Requested-With', 'XMLHttpRequest');
    return this.httpClient.post(this.connectionString + 'templates', template, { headers, observe: 'response' })
  }

  public postSheet(sheet: Sheet): Observable<any> {
    const headers = new HttpHeaders()
    .set('Content-type', 'application/json')
    .set('Authorization', `Bearer ${this.cookieService.get('apiToken')}`)
    .set('Authenticationtoken', `Bearer ${this.cookieService.get('apiToken')}`)
    .set('X-Requested-With', 'XMLHttpRequest');
    return this.httpClient.post(this.connectionString + 'sheets', sheet, {headers, observe: 'response'});
  }
}

export interface Login {
  username: string;
  password: string;
}

export interface Register extends Login {
  email: string;
}

export interface Template {
  templateId: string;
  formTemplates: FormTemplate[];
}

export interface FormTemplate {
  formTemplateId: string;
  type: string;
  title: string;
  x: number;
  y: number;
  height: number;
  width: number;
  labels: string[];
}

export interface Sheet {
  sheetId: string;
  formGroups: FormGroup[];
}
export interface FormGroup
{
  formTemplate: FormTemplate;
  formInputs: string[];
}