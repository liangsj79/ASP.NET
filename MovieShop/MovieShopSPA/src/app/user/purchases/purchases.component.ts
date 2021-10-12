import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { UserService } from 'src/app/core/services/user.service';
import { MovieCard } from 'src/app/shared/models/movieCard';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {
  movieCards!: MovieCard[];
  isLoggedIn: boolean = false;
  constructor(private userService: UserService, private authService:AuthenticationService) { }

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe(resp => this.isLoggedIn = resp);
    this.authService.checkIsLoggedIn();
    if(this.isLoggedIn){
      this.userService.GetUserPurchases()
      .subscribe(
        m => {
          this.movieCards = m;
        }
      )
    }
    
  }

}
