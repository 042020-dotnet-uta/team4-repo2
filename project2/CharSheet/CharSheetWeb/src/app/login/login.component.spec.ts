import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthService, GoogleLoginProvider } from "angularx-social-login";
import { LoginComponent } from './login.component';
import { SocialUser } from "angularx-social-login";
import { Observable } from 'rxjs';
import { DebugElement } from '@angular/core';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let de: DebugElement;

  let googleStub: Partial<GoogleLoginProvider>;
  let authStub: Partial<AuthService>;
  let userStub: Observable<SocialUser>;
  let states: Observable<string[]>;

  beforeEach(async(() => {

    authStub = { readyState: states, authState: userStub};

    TestBed.configureTestingModule({
      declarations: [LoginComponent],
      providers: [{ provide: GoogleLoginProvider, useValue: googleStub },
        { provide: AuthService, useValue: authStub },
        { provide: SocialUser, useValue: userStub },]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    de = fixture.debugElement;

    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy()
  });
});
