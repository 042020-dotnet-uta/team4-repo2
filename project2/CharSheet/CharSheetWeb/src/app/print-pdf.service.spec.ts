import { TestBed } from '@angular/core/testing';

import { SavePdfService } from './print-pdf.service';

describe('PrintPdfService', () => {
  let service: SavePdfService;
  let pdfSpy: any;
  let htmlEl: HTMLElement;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SavePdfService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call captureScreen', () => {
    pdfSpy = spyOn(service, 'captureScreen');
    service.captureScreen(htmlEl);
    expect(pdfSpy).toHaveBeenCalled();
  });

});
