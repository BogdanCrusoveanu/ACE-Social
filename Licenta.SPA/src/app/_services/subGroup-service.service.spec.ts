/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SubGroupServiceService } from './subGroup-service.service';

describe('Service: SubGroupService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SubGroupServiceService]
    });
  });

  it('should ...', inject([SubGroupServiceService], (service: SubGroupServiceService) => {
    expect(service).toBeTruthy();
  }));
});
