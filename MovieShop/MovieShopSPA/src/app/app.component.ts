import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Movie Shop Angular App';

  constructor(private authService: AuthenticationService){}

 ngOnInit():void{
    this.authService.populateUserInfo();
  }
}
