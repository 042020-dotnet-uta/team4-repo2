import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessCharSheetsComponent } from './access-char-sheets.component';

describe('AccessCharSheetsComponent', () => {
  let component: AccessCharSheetsComponent;
  let fixture: ComponentFixture<AccessCharSheetsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessCharSheetsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessCharSheetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
