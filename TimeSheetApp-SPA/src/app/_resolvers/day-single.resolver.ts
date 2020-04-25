import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { Day } from '../_models/Day';
import { GeneralService } from '../_services/general.service';

@Injectable ()
export class DaySingleResolver implements Resolve<Day> {
    constructor(private userService: UserService, private router: Router, private alertify: AlertifyService,
                private authService: AuthService, private generalService: GeneralService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Day> {
        const currentDate = this.generalService.dateNow();

        return this.userService.getDaysForUser(this.authService.decodedToken.nameid, currentDate, currentDate).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving your data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}