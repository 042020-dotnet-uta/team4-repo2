import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
//imports the DragDropModule that allows for "easier" drag and drop funcionality
// for divs.
//import { DragDropModule } from '@angular/cdk/drag-drop';
// imports teh resizing module that allows for "more simple" resizing of divs
//import { ResizeEvent } from 'angular-resizable-element';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ApiService, Template, FormTemplate } from '../api.service';

//ties component info to correlating temlate and css files.
var document;

@Component({
  selector: 'app-create-form',
  templateUrl: './create-form.component.html',
  styleUrls: ['./create-form.component.css']
})

//Angular class for declaring methods, kinda like controller from what I have seen, with the methods inside being like
//controller actions. 
export class CreateFormComponent implements OnInit {
  @ViewChild('formBoundary') formBoundary: ElementRef;
  titleElements = [] as FormElement[];
  formElements = [] as FormElement[];
  width = 50;
  height = 30;
  constructor(private route: ActivatedRoute, private apiService: ApiService
  ) { }
  // create a method to be used in the HTML to  push a new dev of the predefined type
  // to the dom and to the page dynamically 
  createDragItem(): void {
    this.formElements.push({ height: 300, width: 200 } as FormElement);
  }

  createTitleItem(): void {
    this.titleElements.push({ width: this.width, height: this.height } as FormElement);
  }

  changeWidth(w: number): void {
    this.width = w;
  }

  changeHeight(h: number): void {
    this.height = h;
  }

  name;
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.name = params['name'];
    });
  }

  convertToModel(): Template {
    let forms = Array.from(this.formBoundary.nativeElement.children) as Array<HTMLElement>;
    let template = {} as Template;
    template.formTemplates = [];
    forms.forEach(form => {
      let formTemplate = {} as FormTemplate;

      let formArea = (form as HTMLElement).firstChild as HTMLElement;
      let classes = form.className as string;
      let style = window.getComputedStyle(formArea);

      // x and y position from transform.
      let matrix = new WebKitCSSMatrix(style.webkitTransform);
      formTemplate.x = matrix.m41;
      formTemplate.y = matrix.m42;

      formTemplate.width = parseInt(style.width, 10);
      formTemplate.height = parseInt(style.height, 10);

      if (classes.includes("textForm")) {
        formTemplate.title = "";
        formTemplate.type = "text";
        formTemplate.labels = [];
      } else if (classes.includes("titleForm")) {
        formTemplate.title = (formArea as HTMLInputElement).value;
        formTemplate.type = "title";
        formTemplate.labels = [];
      }
      template.formTemplates.push(formTemplate);
    });
    return template;
  }

  saveTemplate(): void {
    let template = this.convertToModel();
    this.apiService.postTemplate(template)
    .subscribe(response => {
      if (response.status == 200) {
        console.log(response.body);
      }
    });
  }
}

export class FormElement {
  width: number;
  height: number;
}
