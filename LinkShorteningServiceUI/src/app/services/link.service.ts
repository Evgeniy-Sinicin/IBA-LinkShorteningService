import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BaseService } from '../shared/base.service';
import { Group } from '../models/group';
import { catchError, map } from 'rxjs/operators';
import { AddLinkRequest } from '../models/add-link.model';
import { PageRequest } from '../models/page-request.model';
import { GetGroupLinksPageRequest } from '../models/get-group-links-page-request.model';
import { Link } from '../models/link';
import { GroupLinksResponse } from '../models/group-links-response.model';

@Injectable({ providedIn: 'root' })
export class LinkService extends BaseService {

    constructor(private http: HttpClient) { 
        super();
    }

    addLinksGroup(group: Group) : Observable<any> {
        let headers = new HttpHeaders({'Content-Type' : 'application/json'});
        return this.http.post(`${environment.apiUrl}/links/AddLinksGroup`, JSON.stringify(group), { headers })
            .pipe(catchError(this.handleError));
    }

    getGroup(id: string) : Observable<Group> {
        let headers = new HttpHeaders({'Content-Type' : 'application/json'});
        return this.http.get<Group>(`${environment.apiUrl}/links/GetGroup?id=${id}`, { headers });
    }

    addLink(link: AddLinkRequest) : Observable<any> {
        let headers = new HttpHeaders({'Content-Type' : 'application/json'});
        return this.http.post(`${environment.apiUrl}/links/AddLink`, JSON.stringify(link), { headers })
            .pipe(catchError(this.handleError));
    }

    getGroups(request: PageRequest) : Observable<Group[]> {
        let headers = new HttpHeaders({'Content-Type' : 'application/json'});
        return this.http.post<Group[]>(`${environment.apiUrl}/links/GetGroups`, JSON.stringify(request), { headers })
            .pipe(catchError(this.handleError));
    }

    getGroupLinks(request: GetGroupLinksPageRequest) : Observable<GroupLinksResponse> {
        let headers = new HttpHeaders({'Content-Type' : 'application/json'});
        return this.http.post<GroupLinksResponse>(`${environment.apiUrl}/links/GetGroupLinks`, JSON.stringify(request), { headers })
            .pipe(catchError(this.handleError));
    }

    getUserLinks(request: PageRequest) : Observable<Link[]> {
        let headers = new HttpHeaders({'Content-Type' : 'application/json'});
        return this.http.post<Link[]>(`${environment.apiUrl}/links/GetUserLinks`, JSON.stringify(request), { headers })
            .pipe(catchError(this.handleError));
    }

    getLinkUrl(id: string) : Observable<string> {
        let headers = new HttpHeaders({'Content-Type' : 'application/json'});
        return this.http.get(`${environment.apiUrl}/links/GetLinkUrl?id=${id}`, { headers })
            .pipe(map((response: Link) => response.url));
    }
}