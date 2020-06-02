import { Component, OnInit } from '@angular/core';
import { AccessCharSheetService } from "./access-char-sheet.service";


@Component({
  selector: 'app-access-char-sheets',
  templateUrl: './access-char-sheets.component.html',
  styleUrls: ['./access-char-sheets.component.css']
})
export class AccessCharSheetsComponent implements OnInit {

  constructor(private charSheet:AccessCharSheetService) { }

  ngOnInit(): void {
    this.charSheet.getAllTemplates();

  }

}
