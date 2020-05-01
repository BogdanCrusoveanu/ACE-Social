import { AuthService } from './../_services/auth.service';
import { CourseService } from './../_services/course.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Course } from '../_models/course';

@Injectable()
export class CourseResolver implements Resolve<Course> {
    constructor(private router: Router,
        private alertify: AlertifyService,
        private courseService: CourseService,
        private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Course> {
        return this.courseService.gerCourses(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                //this.router.navigate(['/admin']);
                return of(null);
            })
        );
    }
}