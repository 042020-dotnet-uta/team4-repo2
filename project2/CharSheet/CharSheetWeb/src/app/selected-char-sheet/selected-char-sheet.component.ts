import { Component, OnInit } from '@angular/core';
import { ApiService, Template, FormTemplate } from '../api.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

  @Component({
  selector: 'app-selected-char-sheet',
  templateUrl: './selected-char-sheet.component.html',
  styleUrls: ['./selected-char-sheet.component.css']
})
export class SelectedCharSheetComponent implements OnInit {
   
    constructor(private route: ActivatedRoute,private router:Router) { }
    template;
    id;
    ngOnInit(): void {
     

    }

}
