import { AuthService } from './../_services/auth.service';
import { Activity } from './../_models/activity';
import { ActivityService } from './../_services/activity.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { UserService } from './../_services/user.service';
import { User } from './../_models/user';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class ActivitiesResolver implements Resolve<Activity> {
    constructor(private activityService: ActivityService,
        private router: Router,
        private alertify: AlertifyService,
        private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Activity> {
        return this.activityService.getActivitiesForUser(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/calendar']);
                return of(null);
            })
        );
    }
}