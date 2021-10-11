import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from 'src/environments/environment';
import { RegisterRequestModel } from '../../shared/models/RegisterRequestModel';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http:HttpClient) { }

  register(requestModel: RegisterRequestModel){
    return this.http.post(`${environment.apiUrl}/api/Account/register`,{
      "email":requestModel.email,
      "password":requestModel.password,
      "firstName":requestModel.firstName,
      "lastName":requestModel.lastName,
      "dateOfBirth":requestModel.dateOfBirth
    });
  }
}
