import { TestBed } from '@angular/core/testing';

import { VetrinaDolciWebapiService } from './vetrina-dolci-webapi.service';

describe('VetrinaDolciWebapiService', () => {
  let service: VetrinaDolciWebapiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VetrinaDolciWebapiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
