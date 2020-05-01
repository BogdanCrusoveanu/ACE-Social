/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SpecializationService } from './specialization.service';

describe('Service: Specialization', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SpecializationService]
    });
  });

  it('should ...', inject([SpecializationService], (service: SpecializationService) => {
    expect(service).toBeTruthy();
  }));
});
