import { TestBed } from '@angular/core/testing';

import { SwitchLanguageService } from './switch-language.service';

describe('SwitchLanguageService', () => {
  let service: SwitchLanguageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SwitchLanguageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
