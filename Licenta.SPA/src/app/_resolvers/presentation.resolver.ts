import { AuthService } from './../_services/auth.service';
import { PresentationService } from './../_services/presentation.service';
import { Presentation } from './../_models/presentation';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class PresentationResolver implements Resolve<Presentation> {
    constructor(
        private alertify: AlertifyService,
        private presentationService: PresentationService,
        private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Presentation> {
        return this.presentationService.getPresentation(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                return of(null);
            })
        );
    }
}