import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ApiService {
  constructor(private httpClient: HttpClient) { }

  public userLogin(login: Login): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post('https://revatureprojectapi.azurewebsites.net/api/account/login', login, {headers, responseType: 'text', observe: 'response' });
  }

  public register(register: Register): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post('https://revatureprojectapi.azurewebsites.net/api/account/register', register, { observe: 'response' });
  }

  public getTemplate(templateId: string): Observable<object> {
    return this.httpClient.get(`https://revatureprojectapi.azurewebsites.net/api/templates/${templateId}`, { observe: 'response' })
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
  'formTemplates': FormTemplate[]
}

export interface FormTemplate {
  type: string;
  title: string;
  x: number;
  y: number;
  height: number;
  width: number;
  labels: string[];
}