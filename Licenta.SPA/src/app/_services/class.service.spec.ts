/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ClassService } from './class.service';

describe('Service: Class', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClassService]
    });
  });

  it('should ...', inject([ClassService], (service: ClassService) => {
    expect(service).toBeTruthy();
  }));
});
