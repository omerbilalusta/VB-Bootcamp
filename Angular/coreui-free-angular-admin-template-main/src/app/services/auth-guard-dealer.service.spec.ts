import { TestBed } from '@angular/core/testing';

import { AuthGuardDealerService } from './auth-guard-dealer.service';

describe('AuthGuardDealerService', () => {
  let service: AuthGuardDealerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthGuardDealerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
