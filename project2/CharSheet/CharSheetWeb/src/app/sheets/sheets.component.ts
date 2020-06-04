import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ApiService, Template, Sheet, FormTemplate, FormGroup } from '../api.service';
import { FormElementArrays } from '../shared/form-types'

@Component({
  selector: 'app-sheets',
  templateUrl: './sheets.component.html',
  styleUrls: ['./sheets.component.css']
})
export class SheetsComponent implements OnInit, FormElementArrays {
  @ViewChild('formBoundary') formBoundary: ElementRef;

  textElements = [];
  titleElements = [];

  currentTemplate: Template;
  currentSheet: Sheet;

  constructor(private apiService: ApiService) {

  }

  ngOnInit(): void {
  }

  fetchTemplate(id: string): void {
    this.apiService.getTemplate(id)
      .subscribe(response => {
        if (response.status == 200) {
          this.currentTemplate = response.body;
          console.log(response);
          this.loadTemplate();
        }
      });
  }

  loadTemplate() {
    let formTemplates = this.currentTemplate.formTemplates;
    formTemplates.forEach(formTemplate => {
      switch (formTemplate.type) {
        case "text":
          this.textElements.push(formTemplate);
          break;
        case "title":
          this.textElements.push(formTemplate);
          break;
      }
    });
  }

  fetchSheet(id: string): void {
    this.apiService.getSheet(id)
      .subscribe(response => {
        if (response.status == 200) {
          this.currentSheet = response.body;
          console.log(this.currentSheet);
        }
      })
  }
}
