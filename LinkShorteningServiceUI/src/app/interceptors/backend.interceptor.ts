import { Injectable, Injector } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

const user: User = {
    "access_token": "FakeToken",
    "name": "FakeName",
    "email": "FakaEmail"
}

@Injectable()
export class BackendInterceptor implements HttpInterceptor {
    constructor(private injector: Injector) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (request.method === "POST" && request.url === `${environment.apiUrl}/api/accounts`) {
            return of(new HttpResponse({
                status: 200,
                body: user
            }));
        }

        next.handle(request);
    }
}