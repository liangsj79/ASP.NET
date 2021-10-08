import { MovieService } from './../../core/services/movie.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/shared/models/movie';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movieDetails!: Movie;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    // this.route.paramMap.subscribe( p => {
    //   this.id =Number(p.get("id"));
    //   this.movieServie.getMovieDetails(this.id)
    //   .subscribe(m =>{
    //     this.movieDetails = m;
    //   })
    //   console.log("Movie Id from the URL: " + this.id);
    // })
    this.route.data.subscribe((response) => {
      this.movieDetails = response.movieDetails;
    });
    
  }

}
