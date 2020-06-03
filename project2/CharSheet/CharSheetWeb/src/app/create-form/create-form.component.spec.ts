import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateFormComponent } from './create-form.component';
//  import dependecies
import { RouterTestingModule } from '@angular/router/testing';
import { ApiService } from '../api.service';

describe('CreateFormComponent', () => {
  let component: CreateFormComponent;
  let fixture: ComponentFixture<CreateFormComponent>;
  let apiStub: any;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateFormComponent],
      providers: [
        { provide: ApiService, useValue: apiStub },
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
});
