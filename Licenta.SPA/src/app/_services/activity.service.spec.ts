/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ActivityService } from './activity.service';

describe('Service: Activity', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ActivityService]
    });
  });

  it('should ...', inject([ActivityService], (service: ActivityService) => {
    expect(service).toBeTruthy();
  }));
});
