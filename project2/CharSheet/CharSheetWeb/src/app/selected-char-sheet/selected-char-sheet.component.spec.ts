import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedCharSheetComponent } from './selected-char-sheet.component';

describe('SelectedCharSheetComponent', () => {
  let component: SelectedCharSheetComponent;
  let fixture: ComponentFixture<SelectedCharSheetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectedCharSheetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectedCharSheetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
