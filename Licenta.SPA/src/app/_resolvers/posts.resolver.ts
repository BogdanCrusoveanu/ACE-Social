import { PostService } from './../_services/post.service';
import { Post } from './../_models/post';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class PostsResolver implements Resolve<Post> {
    constructor(private alertify: AlertifyService,
        private postService: PostService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Post> {
        return this.postService.getAllPosts().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                return of(null);
            })
        );
    }
}