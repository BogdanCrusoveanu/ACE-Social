import { AuthService } from './../_services/auth.service';
import { SeminarService } from './../_services/seminar.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Seminar } from '../_models/seminar';

@Injectable()
export class SeminarResolver implements Resolve<Seminar> {
    constructor(private router: Router,
        private alertify: AlertifyService,
        private seminarService: SeminarService,
        private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Seminar> {
        return this.seminarService.getSeminars(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                //this.router.navigate(['/admin']);
                return of(null);
            })
        );
    }
}