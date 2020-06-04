import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ApiService, Template, Sheet, FormTemplate, FormGroup } from '../api.service';

@Component({
  selector: 'app-sheets',
  templateUrl: './sheets.component.html',
  styleUrls: ['./sheets.component.css']
})
export class SheetsComponent implements OnInit {
  @ViewChild('formBoundary') formBoundary: ElementRef;
  
  currentTemplate: Template;
  currentSheet: Sheet;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
  }

  fetchTemplate(id: string): void
  {
    this.apiService.getTemplate(id)
      .subscribe(response => {
        if (response.status == 200)
        {
          this.currentTemplate = response.body;
          console.log(this.currentTemplate);
        }
      });
  }

  loadTempalte()
  {
    let formTemplates = this.currentTemplate.formTemplates;
  }

  fetchSheet(id: string): void
  {
    this.apiService.getSheet(id)
    .subscribe(response => {
      if (response.status == 200)
      {
        this.currentSheet = response.body;
        console.log(this.currentSheet);
      }
    })
  }
}
