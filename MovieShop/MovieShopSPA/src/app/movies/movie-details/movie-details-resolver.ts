import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import { ActivatedRoute } from '@angular/router';
import { MovieService } from './../../core/services/movie.service';
import { Movie } from 'src/app/shared/models/movie';

@Injectable({
    providedIn: 'root'
})
export class MovieDetailsResolver implements Resolve<Movie>{

    id: number =0;
    constructor(private route: ActivatedRoute,private movieServie: MovieService) { }
    resolve(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<Movie> {
    return this.movieServie.getMovieDetails(Number(route.paramMap.get('id')));

  }
}