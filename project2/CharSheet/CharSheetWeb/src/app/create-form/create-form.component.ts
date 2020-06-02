import { Component, OnInit, ElementRef } from '@angular/core';
//imports the DragDropModule that allows for "easier" drag and drop funcionality
// for divs.
import { DragDropModule } from '@angular/cdk/drag-drop';
// imports teh resizing module that allows for "more simple" resizing of divs
import { ResizeEvent } from 'angular-resizable-element';

//ties component info to correlating temlate and css files. 
var document;

@Component({
  selector: 'app-create-form',
  templateUrl: './create-form.component.html',
  styleUrls: ['./create-form.component.css']
})

//Angular class for declaring methods, kinda like controller from what I have seen, with the methods inside being like
//controller actions. 
export class CreateFormComponent implements OnInit
{
  formElements = [];
  
  // create a method to be used in the HTML to  push a new dev of the predefined type
  // to the dom and to the page dynamically 
  createDragItem(): void {
    this.formElements.push(this.formElements.length);
    document.getElementById("form-boundary").appendChild(this.formElements);
  }


  ngOnInit(): void {}
}
