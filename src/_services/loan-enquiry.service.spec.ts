import { TestBed } from '@angular/core/testing';

import { LoanEnquiryService } from './loan-enquiry.service';

describe('LoanEnquiryService', () => {
  let service: LoanEnquiryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoanEnquiryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
