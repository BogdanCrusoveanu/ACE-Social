/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LaboratoryService } from './laboratory.service';

describe('Service: Laboratory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LaboratoryService]
    });
  });

  it('should ...', inject([LaboratoryService], (service: LaboratoryService) => {
    expect(service).toBeTruthy();
  }));
});
