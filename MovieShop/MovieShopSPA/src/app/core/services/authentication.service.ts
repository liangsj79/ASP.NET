import { Login } from './../../shared/models/login';
import { Injectable, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import {environment} from 'src/environments/environment';
import { Register } from '../../shared/models/register';
import { map,} from 'rxjs/operators';
import { User } from 'src/app/shared/models/user';
import {JwtHelperService} from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject = new BehaviorSubject<User>({} as User); 

  public currentUser = this.currentUserSubject.asObservable();

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn = this.isLoggedInSubject.asObservable();
  constructor(private http:HttpClient) { }
  private jwtHelper =  new JwtHelperService();



  login(userLogin: Login): Observable<boolean> {

    // take email/password from login component and post it to api/account/login URL
    // if we get 200 OK status from API, email/password is correct, so we get token from API
    // store the token in localstorage
    // return true to component

    return this.http.post(`${environment.apiUrl}account/login`, userLogin)
      .pipe(map((response: any) => {

        if (response) {
          // save the respone token (JWT) to local storage
          localStorage.setItem('token', response.token);

          // create the observables so taht other components can get notification when user successfully login
          // any component can subscirbe to this observables to get the notification
          this.populateUserInfo();
          return true;
        }
        return false;
      }));

  }

  populateUserInfo(){
    console.log(this.isLoggedIn);
    var token = localStorage.getItem('token');

    if (token && !this.jwtHelper.isTokenExpired(token)){
      const decodedToken =this.jwtHelper.decodeToken(token);

      // set current user data into Observable
      this.currentUserSubject.next(decodedToken);
      this.isLoggedInSubject.next(true);
    }
  }
  checkIsLoggedIn() {
    
    var token = localStorage.getItem('token');
    if (token && !this.jwtHelper.isTokenExpired(token)){
      const decodedToken =this.jwtHelper.decodeToken(token);

      // set current user data into Observable
      this.currentUserSubject.next(decodedToken);
      this.isLoggedInSubject.next(true);
      
    }
  }


  logout(){
    localStorage.removeItem('token');

    //reset the observables to initial values
    this.currentUserSubject.next({} as User);
    this.isLoggedInSubject.next(false);
  }

  register(userRegister: Register){
    // take the user registration info model {firstName, lastName, dateOfBirth, email, password}
    //post it to api/account
    // if success, redirect to login route

    return this.http.post(`${environment.apiUrl}Account/register`,userRegister);
  }
}
