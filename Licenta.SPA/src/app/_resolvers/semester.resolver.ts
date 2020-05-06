import { SemesterService } from './../_services/semester.service';
import { Semester } from './../_models/semester';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class SemesterResolver implements Resolve<Semester> {
    constructor(
        private alertify: AlertifyService,
        private semesterService: SemesterService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Semester> {
        return this.semesterService.getSemesters().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                return of(null);
            })
        );
    }
}