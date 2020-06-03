import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private httpClient: HttpClient) { }

  public userLogin(username: string, password: string): Observable<any> {
    const headers = new HttpHeaders().set("Content-Type", "application/json");
    return this.httpClient.post('https://revatureprojectapi.azurewebsites.net/api/account/login', { username, password }, { headers, responseType: 'text' });
  }

  public register(username: string, email: string, password: string): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post('https://revatureprojectapi.azurewebsites.net/api/account/register', { username, email, password }, { observe: 'response' });
  }

  public getTemplate(templateId: string): Observable<object> {
    return this.httpClient.get(`https://revatureprojectapi.azurewebsites.net/api/templates/${templateId}`, { observe: 'response' })
  }
}
