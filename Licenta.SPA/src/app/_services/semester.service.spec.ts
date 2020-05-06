/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SemesterService } from './semester.service';

describe('Service: Semester', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SemesterService]
    });
  });

  it('should ...', inject([SemesterService], (service: SemesterService) => {
    expect(service).toBeTruthy();
  }));
});
