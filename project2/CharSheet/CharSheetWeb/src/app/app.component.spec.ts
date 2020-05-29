import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { DebugElement } from '@angular/core';
import { AuthService, GoogleLoginProvider } from "angularx-social-login";

describe('AppComponet', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let de: DebugElement;
  let googleStub: any;
  let authStub: any;

  beforeEach(async(() => {
    authStub = { AuthService: 'anyAuth'};
    googleStub = { caller: 'thissite.com', callee: 'google.com', arguments: 'none' };

    TestBed.configureTestingModule({
      declarations: [AppComponent],
      providers: [{ provide: GoogleLoginProvider, useValue: googleStub },
        {provide: AuthService, useValue: authStub}],

    }).compileComponents(); //compiles html template and css
  }));

  //  setup of variables used for testing
  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    de = fixture.debugElement;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });



});

/*describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Character Sheet Creator'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('Character Sheet Creator');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.content span').textContent).toContain('CharSheetWeb app is running!');
  });
});*/
