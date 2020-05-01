/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DivisionService } from './division.service';

describe('Service: Division', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DivisionService]
    });
  });

  it('should ...', inject([DivisionService], (service: DivisionService) => {
    expect(service).toBeTruthy();
  }));
});
