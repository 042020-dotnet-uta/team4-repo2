import { TestBed } from '@angular/core/testing';

import { SavePdfService } from './print-pdf.service';

describe('PrintPdfService', () => {
  let service: SavePdfService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SavePdfService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
