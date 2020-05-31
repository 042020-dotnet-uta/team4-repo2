import { Component, OnInit, ElementRef } from '@angular/core';
//imports the DragDropModule that allows for "easier" drag and drop funcionality
// for divs.
import { DragDropModule } from '@angular/cdk/drag-drop';
// imports teh resizing module that allows for "more simple" resizing of divs
import { ResizeEvent } from 'angular-resizable-element';

//ties component info to correlating temlates and css files. 
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
  // create a style property to be worked with farther down 
  style: { position: string; left: string; top: string; width: string; height: string; };

  constructor(private draggable: DragDropModule) { }
  //#region Validate resizing to prevent too little of a size
  validate(event: ResizeEvent): boolean {
    const minDimension: number = 50;
    // checks to see if the rectangle width is less than the set minimum constraints
    if (
      event.rectangle.width &&
        event.rectangle.height &&
        (event.rectangle.width < minDimension ||
          event.rectangle.height < minDimension)
    ) {
      // if the resized size is less than the minimum demension restraint return
      // false as the result of the event
      return false;
    }
    // if the size is larger than the minimum dimensions return true as the result of the event
    return true;
  }
  // at the end of resizing  set the "style" positions
  // Utilizing events to check when resizing is finished
  // this should trigger when the resizing check returns true. 
  onResizeEnd(event: ResizeEvent): void {
    this.style = {
      position: 'fixed',
      left: `${event.rectangle.left}px`,
      top: `${event.rectangle.top}px`,
      width: `${event.rectangle.width}px`,
      height: `${event.rectangle.height}px`
    };
  }
  //endResizing Region
  // create a method to be used in the HTML to  push a new dev of the predefined type
  // to the dom and to the page dynamically 
  createDragItem(): void {
    this.formElements.push(this.formElements.length);
  }

  ngOnInit(): void {}
}
