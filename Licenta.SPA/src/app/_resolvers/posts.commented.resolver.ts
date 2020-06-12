import { AuthService } from './../_services/auth.service';
import { PostService } from './../_services/post.service';
import { Post } from './../_models/post';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class CommentedPostsResolver implements Resolve<Post> {
    constructor(private alertify: AlertifyService,
        private postService: PostService,
        private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Post> {
        return this.postService.getPostsWithCurrentUserComment(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                return of(null);
            })
        );
    }
}