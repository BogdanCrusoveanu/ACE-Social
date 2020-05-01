import { AuthService } from './../_services/auth.service';
import { LaboratoryService } from './../_services/laboratory.service';
import { Laboratory } from './../_models/laboratory';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class LaboratoryResolver implements Resolve<Laboratory> {
    constructor(private router: Router,
        private alertify: AlertifyService,
        private laboratoryService: LaboratoryService,
        private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Laboratory> {
        return this.laboratoryService.getLaboratories(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                //this.router.navigate(['/admin']);
                return of(null);
            })
        );
    }
}