import { SubGroupService } from './../_services/subGroup-service.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { SubGroup } from '../_models/subGroup';

@Injectable()
export class SubGroupResolver implements Resolve<SubGroup> {
    constructor(private router: Router,
        private alertify: AlertifyService,
        private subGroupService: SubGroupService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<SubGroup> {
        return this.subGroupService.getSubGroups().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                //this.router.navigate(['/admin']);
                return of(null);
            })
        );
    }
}