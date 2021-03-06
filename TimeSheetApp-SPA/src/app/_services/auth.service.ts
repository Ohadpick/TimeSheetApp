import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject} from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  //photoUrl: BehaviorSubject<string> = new BehaviorSubject<string>('../assets/img/user.png');
  //currentPhotoUrl = this.photoUrl.asObservable();
  photoUrl: string;

  constructor(private http: HttpClient) { }

  // changeMemberPhoto(photoUrl: string) {
  //   this.photoUrl.next(photoUrl);
  // }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((Response: any) => {
        const user = Response;
        if (user) {
          localStorage.setItem('token', user.token);
          //localStorage.setItem('photoUrl', user.photoUrl);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          //this.changeMemberPhoto(user.photoUrl);
        }
      })
    );
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
