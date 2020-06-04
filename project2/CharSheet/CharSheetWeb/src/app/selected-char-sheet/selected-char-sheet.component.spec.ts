import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedCharSheetComponent } from './selected-char-sheet.component';
import { ApiService, Template, FormTemplate } from '../api.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';

describe('SelectedCharSheetComponent', () => {
  let component: SelectedCharSheetComponent;
  let fixture: ComponentFixture<SelectedCharSheetComponent>;
  let apiMock: any;
  let routeMock: any;

  beforeEach(async(() => {
    apiMock = jasmine.createSpyObj('ApiService', ['postTemplate']);
    TestBed.configureTestingModule({
      declarations: [SelectedCharSheetComponent],
      providers: [
        { provide: ApiService, useValue: apiMock },
        {provide: ActivatedRoute, useValue: routeMock},
      ],
      imports: [
        RouterTestingModule,
      ]
      
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

  it('should contain undefined template property', () => {
    expect(component.template).toBeUndefined();
  });

  it('should contain undefined id property', () => {
    expect(component.id).toBeUndefined();
  });

});
