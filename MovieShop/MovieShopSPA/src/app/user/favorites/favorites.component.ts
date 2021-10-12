import { AuthenticationService } from 'src/app/core/services/authentication.service';
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
  isLoggedIn: boolean = false;
  constructor(private userService: UserService, private authService:AuthenticationService) { }

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe(resp => this.isLoggedIn = resp);
    this.authService.checkIsLoggedIn();
    if(this.isLoggedIn){
      this.userService.GetUserFavorites()
      .subscribe(
        m => {
          this.movieCards = m;
        }
      )
    }
    
  }

}
