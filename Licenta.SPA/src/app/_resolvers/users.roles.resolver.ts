import { AdminService } from './../_services/admin.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertifyService } from './../_services/alertify.service';
import { User } from './../_models/user';
import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()
export class UserRolesResolver implements Resolve<User[]> {
    pageNumber = 1;
    pageSize = 20;

    constructor(private adminService: AdminService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot) : Observable<User[]> {
        return this.adminService.getUsersWithRoles(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                //this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}