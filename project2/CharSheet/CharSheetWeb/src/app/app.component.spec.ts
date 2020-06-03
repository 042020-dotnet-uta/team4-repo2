import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { DebugElement } from '@angular/core';
import { AuthService, GoogleLoginProvider } from "angularx-social-login";
import { By } from '@angular/platform-browser';
import { applySourceSpanToExpressionIfNeeded } from '@angular/compiler/src/output/output_ast';
import { ApiService } from './api.service';

describe('AppComponet', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let de: DebugElement;
  let googleStub: any;
  let authStub: any;
  let signInSpy: any;
  let signOutSpy: any;
  let apiStub: any;

  beforeEach(async(() => {
    authStub = { AuthService: 'anyAuth'};
    googleStub = { caller: 'thissite.com', callee: 'google.com', arguments: 'none' };
    apiStub = {apiService: 'anyservice'}
    TestBed.configureTestingModule({
      declarations: [AppComponent],
      providers: [{ provide: GoogleLoginProvider, useValue: googleStub },
        { provide: AuthService, useValue: authStub },
        { provide: ApiService, useValue: apiStub}],
      
    }).compileComponents(); //compiles html template and css

  }));

  //  setup of variables used for testing
  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    de = fixture.debugElement;
    signInSpy = spyOn(component, 'singIn');
    signOutSpy = spyOn(component, 'signOut');
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // check title of app button
  it('should have title: Character Sheet Creator', () => {
    expect(component.title).toContain('Character Sheet Creator');
  });

  it('should have undefined user property', () => {
    expect(component.user).toBeUndefined();
  });

  it('signIn should be called', () => {
    component.singIn('google');
    expect(signInSpy).toHaveBeenCalled();
  });

  it('signOut should be called', () => {
    component.signOut();
    expect(signOutSpy).toHaveBeenCalled();
  });

  it('should have button', () => {
    const linkDes = de.queryAll(
      By.css('button')
    );
    expect(linkDes.length>=1).toBeTruthy();
  });

  it('should have create form link', () => {
    const linkDes = de.queryAll(
      By.css('li')
    );
    const createForm: HTMLDListElement = linkDes[0].nativeElement;
    expect(createForm.textContent).toContain('Create Form');
  });

  it('should have access form link', () => {
    const linkDes = de.queryAll(
      By.css('li')
    );
    const createForm: HTMLDListElement = linkDes[1].nativeElement;
    expect(createForm.textContent).toContain('Access Form');
  });

  it('should have Sign in with Google link', () => {
    const linkDes = de.queryAll(
      By.css('li')
    );
    const createForm: HTMLDListElement = linkDes[2].nativeElement;
    expect(createForm.textContent).toContain('Sign in with Google');
  });

  it('should have Sign out link', () => {
    const linkDes = de.queryAll(
      By.css('li')
    );
    const createForm: HTMLDListElement = linkDes[3].nativeElement;
    expect(createForm.textContent).toContain('Sign out');
  });

  it('should have user div', () => {
    const linkDes = de.queryAll(
      By.css('div')
    );
    expect(linkDes.length>=2).toBeTruthy();
  });

  it('should contain one header', () => {
    const linkDes = de.queryAll(
      By.css('header')
    );
    expect(linkDes.length==1).toBeTruthy();
  });


});

