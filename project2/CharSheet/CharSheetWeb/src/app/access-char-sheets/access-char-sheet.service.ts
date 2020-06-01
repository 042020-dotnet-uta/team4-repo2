import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccessCharSheetService
{
  //private templateURL = 'https://johnssite.azurewebsites.net/api/templates';
  constructor(private http: HttpClient) { }

  getAllTemplates() {
    this.http.get("http://jsonplaceholder.typicode.com/users")
      .subscribe((data) => { console.log(data); });
  }
}