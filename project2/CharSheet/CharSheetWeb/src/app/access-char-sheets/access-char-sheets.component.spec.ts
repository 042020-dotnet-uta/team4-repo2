import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessCharSheetsComponent } from './access-char-sheets.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AccessCharSheetsComponent', () => {
  let component: AccessCharSheetsComponent;
  let fixture: ComponentFixture<AccessCharSheetsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AccessCharSheetsComponent],
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessCharSheetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
