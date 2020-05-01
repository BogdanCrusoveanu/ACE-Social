import { GroupService } from './../_services/group-service.service';
import { Group } from './../_models/group';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class GroupResolver implements Resolve<Group> {
    constructor(private router: Router,
        private alertify: AlertifyService,
        private groupService: GroupService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<Group> {
        return this.groupService.getGroups().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                //this.router.navigate(['/admin']);
                return of(null);
            })
        );
    }
}