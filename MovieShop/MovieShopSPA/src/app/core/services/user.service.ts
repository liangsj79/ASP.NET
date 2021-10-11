import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { MovieCard} from 'src/app/shared/models/movieCard';
import {environment} from 'src/environments/environment';
import { Movie } from 'src/app/shared/models/movie';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }
  
  
  GetUserPurchases(): Observable<MovieCard[]> {
    var reqHeader = new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0OTgyMSIsImVtYWlsIjoiYW50cmFAYW50cmEuY29tIiwiZ2l2ZW5fbmFtZSI6ImEiLCJmYW1pbHlfbmFtZSI6ImIiLCJuYmYiOjE2MzM3MDkxNjQsImV4cCI6MTYzMzk2ODM2NCwiaWF0IjoxNjMzNzA5MTY0LCJpc3MiOiJNb3ZpZSBTaG9wLCBJbmMiLCJhdWQiOiJNb3ZpZSBTaG9wIEN1c3RvbWVycyJ9.45gUYWJ451X0Is92MRDigd0M1xER_m_XkqHgLGuI_Pw'
    });
    return this.http.get<MovieCard[]>(`${environment.apiUrl}User/purchases`, { headers: reqHeader });
  }

  GetUserFavorites(): Observable<MovieCard[]> {
    var reqHeader = new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0OTgyMSIsImVtYWlsIjoiYW50cmFAYW50cmEuY29tIiwiZ2l2ZW5fbmFtZSI6ImEiLCJmYW1pbHlfbmFtZSI6ImIiLCJuYmYiOjE2MzM3MDkxNjQsImV4cCI6MTYzMzk2ODM2NCwiaWF0IjoxNjMzNzA5MTY0LCJpc3MiOiJNb3ZpZSBTaG9wLCBJbmMiLCJhdWQiOiJNb3ZpZSBTaG9wIEN1c3RvbWVycyJ9.45gUYWJ451X0Is92MRDigd0M1xER_m_XkqHgLGuI_Pw'
    });
    return this.http.get<MovieCard[]>(`${environment.apiUrl}User/favorites`, { headers: reqHeader });
  }

  GetUserReviews(): Observable<MovieCard[]> {
    var reqHeader = new HttpHeaders({ 
      'Content-Type': 'application/json',
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0OTgyMSIsImVtYWlsIjoiYW50cmFAYW50cmEuY29tIiwiZ2l2ZW5fbmFtZSI6ImEiLCJmYW1pbHlfbmFtZSI6ImIiLCJuYmYiOjE2MzM3MDkxNjQsImV4cCI6MTYzMzk2ODM2NCwiaWF0IjoxNjMzNzA5MTY0LCJpc3MiOiJNb3ZpZSBTaG9wLCBJbmMiLCJhdWQiOiJNb3ZpZSBTaG9wIEN1c3RvbWVycyJ9.45gUYWJ451X0Is92MRDigd0M1xER_m_XkqHgLGuI_Pw'
    });
    return this.http.get<MovieCard[]>(`${environment.apiUrl}User/reviews`, { headers: reqHeader });
  }
}
