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
  constructor(private userService: UserService) { }

  ngOnInit(): void {


    this.userService.GetUserPurchases()
    .subscribe(
      m => {
        this.movieCards = m;
      }
    )
    
    
  }

}
