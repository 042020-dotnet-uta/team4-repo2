import { Component, OnInit } from '@angular/core';
import { SavePdfService } from '../print-pdf.service'

@Component({
  selector: 'app-selected-sheet',
  templateUrl: './selected-sheet.component.html',
  styleUrls: ['./selected-sheet.component.css']
})
export class SelectedSheetComponent implements OnInit {

  constructor(private savePdfService: SavePdfService) { }

  ngOnInit(): void {
  }

  saveToPdf(){
    this.savePdfService.captureScreen();
  }

}
