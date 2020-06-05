import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSheetComponent } from './create-sheet.component';
import { ApiService, Template, Sheet, } from '../api.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing'

describe('CreateSheetComponent', () => {
  let component: CreateSheetComponent;
  let fixture: ComponentFixture<CreateSheetComponent>;
  let fetchTemplateSpy: any;
  let loadTemplateSpy: any;
  let fetchSheetSpy: any;
  let templateStub: Partial<Template>;
  
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateSheetComponent],
      providers: [
        ApiService,
      ],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule
      ]
    })
      .compileComponents();
    templateStub = { name: "testTemplate" };
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSheetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have textElements property', () => {
    expect(component.textElements).toBeTruthy();
  });

  it('should have titleElements property', () => {
    expect(component.titleElements).toBeTruthy();
  });

  it('should have undefined templateId property', () => {
    expect(component.templateId).toBeUndefined();
  });

  it('should have undefined sheetId property', () => {
    expect(component.sheetId).toBeUndefined();
  });

  it('should call fetchTemplate', () => {
    fetchTemplateSpy = spyOn(component, 'fetchTemplate');
    component.fetchTemplate();
    expect(fetchTemplateSpy).toHaveBeenCalled();
  });

  it('should call loadTemplate', () => {
    loadTemplateSpy = spyOn(component, 'loadTemplate');
    component.loadTemplate(<Template>templateStub);
    expect(loadTemplateSpy).toHaveBeenCalled();
  });

  it('should call fetchSheet', () => {
    fetchSheetSpy = spyOn(component, 'fetchSheet');
    component.fetchSheet();
    expect(fetchSheetSpy).toHaveBeenCalled();
  });

});
