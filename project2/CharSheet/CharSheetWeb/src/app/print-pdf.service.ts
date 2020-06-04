import { Injectable } from '@angular/core';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';


@Injectable({
  providedIn: 'root'
})
export class SavePdfService {

constructor() { }

 // Method to capture form and convert to PDF
  public captureScreen(){
    var data = document.getElementById('toPdf');
    html2canvas(data).then(canvas=>{
      let pdf = new jspdf('p', 'mm', [canvas.width, canvas.height]);
      var imgData = canvas.toDataURL();
      pdf.addImage(imgData, 0,0);
      pdf.save('myCharSheet.pdf');
      
    });    
  }  
}