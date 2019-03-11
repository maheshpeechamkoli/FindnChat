import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseUrl =  environment.apiUrl + 'auth/';
jwtHelper = new JwtHelperService();
currentUser: User;
decodeToken: any;
photoUrl = new BehaviorSubject<string>('../../assets/user.png');
currentPhotoUrl = this.photoUrl.asObservable();

constructor(@Inject(DOCUMENT) private document: Document, private http: HttpClient) { }

changeMemberPhoto(photoUrl: string) {
  this.photoUrl.next(photoUrl);
}

login(model: any) {
  return this.http.post(this.baseUrl + 'login', model).pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
        localStorage.setItem('user', JSON.stringify(user.user));
        this.decodeToken = this.jwtHelper.decodeToken(user.token);
        this.currentUser = user.user;
        this.changeMemberPhoto(this.currentUser.photoUrl);
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

roleMatch(allowedRoles): boolean {
  let isMatch = false;
  const userRoles = this.decodeToken.role as Array<string>;
  allowedRoles.forEach(element => {
    if (userRoles.includes(element)) {
      isMatch = true;
      return;
    }
  });
  return isMatch;
}

googlelogin() {
  this.document.location.href = environment.apiUrl + 'google/signInWithGoogle';
}
}
