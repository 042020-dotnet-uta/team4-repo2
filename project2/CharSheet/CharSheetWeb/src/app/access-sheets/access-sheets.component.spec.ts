import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessSheetsComponent } from './access-sheets.component';

describe('AccessSheetsComponent', () => {
  let component: AccessSheetsComponent;
  let fixture: ComponentFixture<AccessSheetsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessSheetsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessSheetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
