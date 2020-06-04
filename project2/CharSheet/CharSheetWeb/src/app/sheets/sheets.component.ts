import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { ApiService, Template, Sheet, FormTemplate, FormGroup } from '../api.service';
import { FormElementArrays } from '../shared/form-types'
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-sheets',
  templateUrl: './sheets.component.html',
  styleUrls: ['./sheets.component.css']
})
export class SheetsComponent implements OnInit, AfterViewInit, FormElementArrays {
  @ViewChild('formBoundary') formBoundary: ElementRef;

  textElements = [];
  titleElements = [];

  templateId: string;
  currentTemplate: Template;
  sheetId: string;
  currentSheet: Sheet;

  constructor(private apiService: ApiService, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(paramsId => {
      this.templateId = paramsId.p1;
    });
  }

  ngAfterViewInit(): void {
    if (this.templateId != null)
      this.fetchTemplate();
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

  convertToNewModel(): Sheet {
    let sheet = {} as Sheet;
    let forms = Array.from(this.formBoundary.nativeElement.children) as Array<HTMLElement>;
    sheet.formGroups = [];
    forms.forEach(form => {
      let formGroup = {} as FormGroup;
      formGroup.formTemplateId = form.id;
      formGroup.formInputs = [] as string[];

      let classes = form.className as string;
      if (classes.includes("text-form")) {
        let input = (form.firstChild as HTMLInputElement).value;
        formGroup.formInputs.push(input);
      sheet.formGroups.push(formGroup);
      }
    });
    return sheet;
  }

  saveNewSheet() {
    let sheet: Sheet;
    if (this.sheetId == null)
      sheet = this.convertToNewModel();
    this.apiService.postSheet(sheet)
      .subscribe(response => {
        console.log(response);
      });
  }
}
