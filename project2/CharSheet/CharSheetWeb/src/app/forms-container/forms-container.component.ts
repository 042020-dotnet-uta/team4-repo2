import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import 'jqueryui';

import { FormTemplate } from '../shared/form-template';
import { GenerateFormService } from '../generate-form.service';

@Component({
  selector: 'app-forms-container',
  templateUrl: './forms-container.component.html',
  styleUrls: ['./forms-container.component.css']
})
export class FormsContainerComponent implements OnInit {

  constructor(private service: GenerateFormService) { 
  }

  ngOnInit(): void {
  }

  newFormTemplate(): void {
    let element = document.createElement("div");
    ($(element) as any).draggable().resizable();
  }
}
