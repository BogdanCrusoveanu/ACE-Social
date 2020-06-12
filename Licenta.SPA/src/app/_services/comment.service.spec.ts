/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CommentService } from './comment.service';

describe('Service: Comment', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CommentService]
    });
  });

  it('should ...', inject([CommentService], (service: CommentService) => {
    expect(service).toBeTruthy();
  }));
});
