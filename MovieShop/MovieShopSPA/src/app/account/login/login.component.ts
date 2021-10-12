import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Login } from 'src/app/shared/models/login';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userLogin: Login={
    email:'',
    password:''
  }

  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
  }

  loginSubmit(form:NgForm){
    //capture the email/password from the view
    // then send the modle to the Authentication service

    // console.log(form);
    // console.log('login button clicked');
    // console.log(this.userLogin);

    this.authService.login(this.userLogin)
    .subscribe(
      (response) =>{
        if (response){
          this.router.navigateByUrl('/');
        }
        (err:HttpErrorResponse) =>{
          console.log(err);
        }
      }
      
      // If token is saved successfully, then redirect to home page
      // If error then show error message and stay on same page


    )
  }

}
