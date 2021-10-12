import { Register } from '../../shared/models/register';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private authenticationServive: AuthenticationService) { }

  ngOnInit(): void {
    var requestModel: Register;
  }
  onSubmit(){
    
  }
}
