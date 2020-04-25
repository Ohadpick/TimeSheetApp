import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginationResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Day } from '../_models/Day';
import { GeneralService } from './general.service';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private generalService: GeneralService) { }

  getUsers(page?, itemsPerPage?, userParam?): Observable<PaginationResult<User[]>> {
    const paginatedResult: PaginationResult<User[]> = new PaginationResult<User[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (userParam) {
      params = params.append('orderBy', userParam.orderBy);
    }

    return this.http.get<User[]>(this.baseUrl + 'users', { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  updateUser(id: number, user: User) {
    return this.http.put (this.baseUrl + 'users/' + id, user);
  }

  deletePhoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + id);
  }

  getDaysForUser(userId: number, startDate?: string, endDate?: string): Observable<PaginationResult<Day[]>> {
    const paginatedResult: PaginationResult<Day[]> = new PaginationResult<Day[]>();

    let params = new HttpParams();

    params = params.append('userId', userId.toString());

    if (startDate != null) {
      params = params.append('startDate', startDate);
    }
    if (endDate != null) {
      params = params.append('endDate', endDate);
    }

    return this.http.get<Day[]>(this.baseUrl + 'users/' + userId + '/days', { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  createDay(userId: number, day: Day) {
    debugger;
    return this.http.post(this.baseUrl + 'users/' + userId + '/days', day);
  }

  deleteDay(userId: number, reportedDate: Date) {
    let params = new HttpParams();

    params = params.append('reportedDate', this.generalService.generalDateFormat(reportedDate));

    return this.http.post(this.baseUrl + 'users/' + userId + '/days', params);
  }
}

