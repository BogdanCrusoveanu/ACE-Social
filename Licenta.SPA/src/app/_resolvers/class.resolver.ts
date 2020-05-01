import { ClassService } from './../_services/class.service';
import { Class } from './../_models/class';
import { AuthService } from './../_services/auth.service';
import { ActivityService } from './../_services/activity.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class ClassesResolver implements Resolve<Class> {
    constructor(private router: Router,
        private alertify: AlertifyService,
        private classService: ClassService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Class> {
        return this.classService.getClasses().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/admin']);
                return of(null);
            })
        );
    }
}