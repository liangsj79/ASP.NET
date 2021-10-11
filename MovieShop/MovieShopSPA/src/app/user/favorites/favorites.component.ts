import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { MovieCard } from 'src/app/shared/models/movieCard';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {
  movieCards!: MovieCard[];
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.GetUserFavorites()
      .subscribe(
        m => {
          this.movieCards = m;
          console.log(m);
        }
      )
  }

}
