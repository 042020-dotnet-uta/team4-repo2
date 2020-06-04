import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateFormComponent } from './create-form.component';
//  import dependecies
import { RouterTestingModule } from '@angular/router/testing';
import { ApiService, Template } from '../api.service';

describe('CreateFormComponent', () => {
  let component: CreateFormComponent;
  let fixture: ComponentFixture<CreateFormComponent>;
  let apiMock: any;
 /* let dragSpy: any;
  let titleSpy: any;*/
  let createSpy: any;

  beforeEach(async(() => {
    apiMock = jasmine.createSpyObj('ApiService', ['postTemplate']);
    TestBed.configureTestingModule({
      declarations: [CreateFormComponent],
      providers: [
        { provide: ApiService, useValue: apiMock},
      ],
      imports: [
        RouterTestingModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have default width of 300', () => {
    expect(component.width == 300).toBeTruthy();
  });

  it('should have default height of 100', () => {
    expect(component.height == 100).toBeTruthy();
  });

  it('should have titleElements property', () => {
    expect(component.titleElements).toBeTruthy();
  });

  it('should have textElements property', () => {
    expect(component.textElements).toBeTruthy();
  });
 
  /*it('should call createDragItem', () => {
    dragSpy = spyOn(component, 'createDragItem');
    let drag: any = component.createDragItem();
    expect(dragSpy).toHaveBeenCalled();
    expect(drag).toBeUndefined();
  });*/

  /*it('should call createTitleItem', () => {
    titleSpy = spyOn(component, 'createTitleItem');
    component.createTitleItem();
    expect(titleSpy).toHaveBeenCalled();
  });*/

  /*it('should change width when changeWidth is called', () => {
    expect(component.width == 50).toBeTruthy();
    component.changeWidth(100);
    expect(component.width == 100).toBeTruthy();
  });

  it('should change height when changeHeight is called', () => {
    expect(component.height == 30).toBeTruthy();
    component.changeHeight(200);
    expect(component.height == 200).toBeTruthy();
  });*/

  it('convertToModel should return a Template', () => {
    const template: Template = component.convertToModel();
    expect(template).toBeTruthy();
  });

  it('should call createItem', () => {
    createSpy = spyOn(component, 'createItem');
    component.createItem();
    expect(createSpy).toHaveBeenCalled();
  });
 
});
