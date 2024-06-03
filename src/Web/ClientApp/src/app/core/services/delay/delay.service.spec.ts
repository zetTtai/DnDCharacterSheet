import { TestBed } from '@angular/core/testing';

import { DelayService } from './delay.service';

describe('DelayService', () => {
  let service: DelayService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DelayService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
