import { SpecializationService } from './../_services/specialization.service';
import { Specialization } from './../_models/specialization';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class SpecializationResolver implements Resolve<Specialization> {
    constructor(private router: Router,
        private alertify: AlertifyService,
        private specializationService: SpecializationService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Specialization> {
        return this.specializationService.getSpecializations().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                //this.router.navigate(['/admin']);
                return of(null);
            })
        );
    }
}