import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';

import { BaseService } from "../shared/base.service";
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
providedIn: 'root'
})
export class AuthService extends BaseService {

    // Observable navItem source
    private _authNavStatusSource = new BehaviorSubject<boolean>(false);
    // Observable navItem stream
    authNavStatus$ = this._authNavStatusSource.asObservable();

    private manager = new UserManager(environment.clientSettings as UserManagerSettings);
    private user: User | null;

    constructor(private http: HttpClient, private router: Router) {
        super();

        this.manager.getUser().then(user => {
        this.user = user;
        this._authNavStatusSource.next(this.isAuthenticated());
        });
    }

    login(redirect: string) {
        let url = '';
        if (redirect && redirect.length > 0) {
            url = redirect;
        }
        return this.manager.signinRedirect({state:url});
    }

    async completeAuthentication() {
        this.user = await this.manager.signinRedirectCallback();
        this._authNavStatusSource.next(this.isAuthenticated());
        this.router.navigate([this.user.state]);
    }

    register(userRegistration: any) {
        return this.http.post(environment.identityServerUrl + '/account', userRegistration).pipe(catchError(this.handleError));
    }

    isAuthenticated(): boolean {
        return this.user != null && !this.user.expired;
    }

    get authorizationHeaderValue(): string {
        return `${this.user.token_type} ${this.user.access_token}`;
    }

    get name(): string {
        return this.user != null ? this.user.profile.name : '';
    }

    async signout() {
        await this.manager.signoutRedirect();
    }
}