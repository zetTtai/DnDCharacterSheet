import { TestBed } from '@angular/core/testing';

import { MobileNavigationService } from './mobile-navigation.service';

describe('MobileNavigationService', () => {
  let service: MobileNavigationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MobileNavigationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
