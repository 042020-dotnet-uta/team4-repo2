import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { ApiService, Template, Sheet, FormTemplate, FormGroup } from '../api.service';
import { FormElementArrays } from '../shared/form-types'
import { ActivatedRoute } from '@angular/router';
import { SavePdfService } from '../print-pdf.service';

@Component({
  selector: 'app-create-sheet',
  templateUrl: './create-sheet.component.html',
  styleUrls: ['./create-sheet.component.css']
})
export class CreateSheetComponent implements OnInit, AfterViewInit, FormElementArrays {
  @ViewChild('formBoundary') formBoundary: ElementRef;

  titleTextElements = [];
  textElements = [];
  titleElements = [];

  templateId: string;
  sheetId: string;
  nameInput: string;

  constructor(private apiService: ApiService, private activatedRoute: ActivatedRoute, private savedPdfService: SavePdfService) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(paramsId => {
      this.templateId = paramsId.p1;
      this.sheetId = paramsId.p2;
    });
  }

  ngAfterViewInit(): void {
    if (this.templateId != null)
      this.fetchTemplate();
    else if (this.sheetId != null)
      this.fetchSheet();
  }

  loadTemplate(template: Template): void {
    this.sheetId = null;
    this.templateId = template.templateId;
    this.nameInput = null;

    this.textElements.splice(0, this.textElements.length);
    this.titleElements.splice(0, this.titleElements.length);
    this.titleTextElements.splice(0, this.titleTextElements.length);

    let formTemplates = template.formTemplates;

    formTemplates.forEach(formTemplate => {
      this.pushForm(formTemplate);
    });
  }

  loadSheet(sheet: Sheet): void {
    this.templateId = null;
    this.sheetId = sheet.sheetId;
    this.nameInput = sheet.name;

    this.textElements.splice(0, this.textElements.length);
    this.titleElements.splice(0, this.titleElements.length);
    this.titleTextElements.splice(0, this.titleTextElements.length);

    let formGroups = sheet.formGroups;

    formGroups.forEach(formGroup => {
      formGroup.formTemplate.formInputs = formGroup.formInputs;
      this.pushForm(formGroup.formTemplate);
    });
  }

  pushForm(formTemplate: FormTemplate) {
    switch (formTemplate.type) {
      case "title-text":
        this.titleTextElements.push(formTemplate);
        break;
      case "text":
        this.textElements.push(formTemplate);
        break;
      case "title":
        this.titleElements.push(formTemplate);
        break;
    }
  }

  fetchSheet(): void {
    this.apiService.getSheet(this.sheetId)
      .subscribe(response => {
        if (response.status == 200) {
          console.log(response.body);
          this.loadSheet(response.body as Sheet);
        }
      })
  }

  fetchTemplate(): void {
    this.apiService.getTemplate(this.templateId)
      .subscribe(response => {
        if (response.status == 200) {
          console.log(response);
          this.loadTemplate(response.body as Template);
        }
      });
  }

  toModel(): Sheet {
    let sheet = {} as Sheet;
    sheet.name = this.nameInput
    let forms = Array.from(this.formBoundary.nativeElement.children) as Array<HTMLElement>;
    sheet.formGroups = [];
    forms.forEach(form => {
      let formGroup = {} as FormGroup;
      formGroup.formTemplateId = form.id;
      formGroup.formInputs = [] as string[];

      let classes = form.className as string;
      if (classes.includes("title-text-form")) {
        let input = ((form.firstChild as HTMLElement).querySelector(':nth-child(2)') as HTMLInputElement).value;
        formGroup.formInputs.push(input);
      } else if (classes.includes("text-form")) {
        let input = (form.firstChild as HTMLInputElement).value;
        formGroup.formInputs.push(input);
      }
      sheet.formGroups.push(formGroup);
    });
    return sheet;
  }

  saveSheet(): void {
    let sheet = this.toModel();
    if (this.sheetId == null) {
      this.apiService.postSheet(sheet)
        .subscribe(response => {
          this.sheetId = response.body.sheetId;
          this.nameInput = response.body.name;
          this.templateId = null;
          console.log(response);
        });
    } else {
      sheet.sheetId = this.sheetId;
      this.apiService.putSheet(sheet)
        .subscribe(response => {
          console.log(response);
        });
    }
  }
  toPdf(): void {
    this.savedPdfService.captureScreen(this.formBoundary.nativeElement);
  }
}
