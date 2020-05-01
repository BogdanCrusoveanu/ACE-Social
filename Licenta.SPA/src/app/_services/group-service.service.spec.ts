/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GroupServiceService } from './group-service.service';

describe('Service: GroupService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GroupServiceService]
    });
  });

  it('should ...', inject([GroupServiceService], (service: GroupServiceService) => {
    expect(service).toBeTruthy();
  }));
});
