/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PresentationService } from './presentation.service';

describe('Service: Presentation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PresentationService]
    });
  });

  it('should ...', inject([PresentationService], (service: PresentationService) => {
    expect(service).toBeTruthy();
  }));
});
