import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable, EMPTY} from 'rxjs';
import { MovieCard} from 'src/app/shared/models/movieCard';
import {environment} from 'src/environments/environment';
import { Movie } from 'src/app/shared/models/movie';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient, private authService: AuthenticationService, private router: Router) { }
  private jwtHelper = new JwtHelperService();
  isLoggedIn: boolean = false;

  GetUserPurchases(): Observable<MovieCard[]> {
    var token = localStorage.getItem('token');
    var reqHeader = new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    });
    return this.http.get<MovieCard[]>(`${environment.apiUrl}User/purchases`, { headers: reqHeader });
  }

  GetUserFavorites(): Observable<MovieCard[]> {
    
    var token = localStorage.getItem('token');
  
    var reqHeader = new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' +  token
    });
    return this.http.get<MovieCard[]>(`${environment.apiUrl}User/favorites`, { headers: reqHeader }); 
  }

  GetUserReviews(): Observable<MovieCard[]> {
    var token = localStorage.getItem('token');
    var reqHeader = new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' +  token
    });
    return this.http.get<MovieCard[]>(`${environment.apiUrl}User/reviews`, { headers: reqHeader });
  }
}
