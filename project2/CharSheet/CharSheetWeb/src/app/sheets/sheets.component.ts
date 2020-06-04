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

  templateId: string;
  currentTemplate: Template;
  sheetId: string;
  currentSheet: Sheet;

  constructor(private apiService: ApiService) {

  }

  ngOnInit(): void {
  }

  fetchTemplate(): void {
    this.apiService.getTemplate(this.templateId)
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

    this.textElements.splice(0, this.textElements.length);
    this.titleElements.splice(0, this.titleElements.length);

    formTemplates.forEach(formTemplate => {
      switch (formTemplate.type) {
        case "text":
          this.textElements.push(formTemplate);
          break;
        case "title":
          this.titleElements.push(formTemplate);
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
