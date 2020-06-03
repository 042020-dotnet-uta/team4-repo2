import { Component, OnInit, ElementRef } from '@angular/core';
//imports the DragDropModule that allows for "easier" drag and drop funcionality
// for divs.
import { DragDropModule } from '@angular/cdk/drag-drop';
// imports teh resizing module that allows for "more simple" resizing of divs
import { ResizeEvent } from 'angular-resizable-element';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

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
  titleElements = [] as FormElement[];
  formElements = [] as FormElement[];
  width = 50;
  height = 30;
  constructor(private route: ActivatedRoute,
  ) { }
  // create a method to be used in the HTML to  push a new dev of the predefined type
  // to the dom and to the page dynamically 
  createDragItem(): void {
    this.formElements.push({ height: 300, width: 200 } as FormElement);
  }

  createTitleItem(): void {
    this.titleElements.push({ width: this.width, height: this.height } as FormElement);
  }

  changeWidth(w:number):void{
    this.width = w;
  }

  changeHeight(h:number):void{
    this.height = h;
  }
  name;
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.name = params['name'];
    });
  }
}

export class FormElement {
  width: number;
  height: number;
}
