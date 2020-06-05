import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessCharSheetsComponent } from './access-char-sheets.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ApiService, Template, Sheet } from '../api.service';

describe('AccessCharSheetsComponent', () => {
  let component: AccessCharSheetsComponent;
  let fixture: ComponentFixture<AccessCharSheetsComponent>;
  let templateStub: Partial<Template>;


  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AccessCharSheetsComponent,
      ],
      providers: [
        ApiService,
      ],
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ]
    })
      .compileComponents();
    templateStub = { name : "testTemplate" };

  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessCharSheetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have default null property selectedTemplate', () => {
    expect(component.selectedTemplate).toBeNull();
  });

  it('should have default null property selectedSheet', () => {
    expect(component.selectedSheet).toBeNull();
  });



});
