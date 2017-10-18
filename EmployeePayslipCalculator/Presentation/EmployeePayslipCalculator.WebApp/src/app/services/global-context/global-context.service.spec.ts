import { TestBed, inject } from '@angular/core/testing';

import { GlobalContextService } from './global-context.service';

describe('GlobalContextService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GlobalContextService]
    });
  });

  it('should be created', inject([GlobalContextService], (service: GlobalContextService) => {
    expect(service).toBeTruthy();
  }));
});
