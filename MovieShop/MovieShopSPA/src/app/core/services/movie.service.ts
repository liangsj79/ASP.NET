import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { MovieCard} from 'src/app/shared/models/movieCard';
import {environment} from 'src/environments/environment';
import { Movie } from 'src/app/shared/models/movie';
@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http:HttpClient) { }

  getTopGrossingMovies(): Observable<MovieCard[]> {
    return this.http.get<MovieCard[]>(`${environment.apiUrl}Movies/toprevenue`);
    
     //  return this.http.get('https://localhost:5001/api/Movies/toprevenue').pipe(map(resp => resp as Moviecard[]));


  }
  getMovieDetails(id: number): Observable<Movie>{
    return this.http.get<Movie>(`${environment.apiUrl}Movies/${id}`);
  }

  
}
