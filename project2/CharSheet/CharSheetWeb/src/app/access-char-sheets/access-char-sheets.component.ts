import { Component, OnInit } from '@angular/core';
import { AccessCharSheetService } from "./access-char-sheet.service";
import{HttpClient} from '@angular/common/http'


@Component({
  selector: 'app-access-char-sheets',
  templateUrl: './access-char-sheets.component.html',
  styleUrls: ['./access-char-sheets.component.css']
})
export class AccessCharSheetsComponent implements OnInit {

  constructor(private http:HttpClient) { }
  httpData;
  ngOnInit(): void {
    this.http.get("http://jsonplaceholder.typicode.com/users")
        .subscribe((data) => this.displayData(data));
  }
  displayData(data) { this.httpData = data; }
}
