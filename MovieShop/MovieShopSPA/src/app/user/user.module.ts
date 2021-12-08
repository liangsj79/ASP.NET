import { SharedModule } from './../shared/shared.module';

import { FavoritesComponent } from './favorites/favorites.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { ReviewsComponent } from './reviews/reviews.component';


@NgModule({
  declarations: [
    UserComponent,
    UserDetailsComponent,
    EditUserComponent,
    PurchasesComponent,
    FavoritesComponent,
    ReviewsComponent,

  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
