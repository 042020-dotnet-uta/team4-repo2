import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';


@Component({
  selector: 'app-access-char-sheets',
  templateUrl: './access-char-sheets.component.html',
  styleUrls: ['./access-char-sheets.component.css']
})
export class AccessCharSheetsComponent implements OnInit {
  constructor(private http:HttpClient,private route:ActivatedRoute) { }
  httpData;
  ngOnInit(): void {
    this.http.get("http://jsonplaceholder.typicode.com/users")
      .subscribe((data) => this.displayData(data));
  }
  displayData(data) { this.httpData = data; }
  
}
