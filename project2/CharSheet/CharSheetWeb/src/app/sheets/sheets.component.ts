import { Component, OnInit } from '@angular/core';
import { ApiService, Template, Sheet, FormTemplate, FormGroup } from '../api.service';

@Component({
  selector: 'app-sheets',
  templateUrl: './sheets.component.html',
  styleUrls: ['./sheets.component.css']
})
export class SheetsComponent implements OnInit {

  currentTemplate: Template;
  currentSheet: Sheet;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
  }

  loadTemplate(id: string): void
  {
    this.apiService.getTemplate(id)
      .subscribe(response => {
        if (response.status == 200)
        {
          this.currentTemplate = response.body;
          console.log(this.currentTemplate);
        }
      });
  }

  loadSheet(id: string): void
  {
    this.apiService.getSheet(id)
    .subscribe(response => {
      if (response.status == 200)
      {
        this.currentSheet = response.body;
        console.log(this.currentSheet);
      }
    })
  }
}
