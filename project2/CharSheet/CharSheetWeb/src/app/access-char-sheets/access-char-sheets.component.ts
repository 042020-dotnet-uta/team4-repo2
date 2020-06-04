import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ApiService, Template, FormTemplate } from '../api.service';



@Component({
  selector: 'app-access-char-sheets',
  templateUrl: './access-char-sheets.component.html',
  styleUrls: ['./access-char-sheets.component.css']
})
export class AccessCharSheetsComponent implements OnInit {
  constructor(private http: HttpClient, private route: ActivatedRoute,
    private apiService: ApiService,private router:Router) { }
  tempId;
  selectedTemplate = null;

  getSelectedTemplate(template: Template) {
    this.selectedTemplate = template;
  }

  httpData;
  ngOnInit(): void {
    this.apiService.getTemplates().subscribe((data) => this.displayData(data.body));
  }
  displayData(data) { this.httpData = data; }
}




