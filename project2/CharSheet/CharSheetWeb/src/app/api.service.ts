import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private httpClient: HttpClient) { }

  public userLogin(username: string, password: string): Observable<object> {
    let responseBody: object;
    this.httpClient.post("https://revatureprojectapi.azurewebsites.net/api/account/login", { username, password } as loginInterface, { observe: 'response' })
      .subscribe(response => {
        if (response.status == 200) {
          responseBody = response.body;
        }
      });
    return of(responseBody);
  }

  public getTemplate(templateId: string): Observable<object> {
    let responseBody: object;
    this.httpClient.get(`https://revatureprojectapi.azurewebsites.net/api/templates/${templateId}`, {observe: 'response'})
    .subscribe(response => {
      if (response.status == 200) {
        responseBody = response.body;
      }
    });
    return of(responseBody);
  }
}

interface loginInterface {
  username: string;
  password: string;
}
