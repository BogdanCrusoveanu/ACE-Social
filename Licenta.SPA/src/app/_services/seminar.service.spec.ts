/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SeminarService } from './seminar.service';

describe('Service: Seminar', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SeminarService]
    });
  });

  it('should ...', inject([SeminarService], (service: SeminarService) => {
    expect(service).toBeTruthy();
  }));
});
