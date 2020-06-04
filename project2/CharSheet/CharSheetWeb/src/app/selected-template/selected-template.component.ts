import { Component, OnInit } from '@angular/core';
import { ApiService, Template, FormTemplate } from '../api.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

  @Component({
  selector: 'app-selected-template',
  templateUrl: './selected-template.component.html',
  styleUrls: ['./selected-template.component.css']
})
export class SelectedTemplateComponent implements OnInit {
   
    constructor(private route: ActivatedRoute,private router:Router) { }
    template;
    id;
    ngOnInit(): void {
     

    }

}
