import { EditUserComponent } from './edit-user/edit-user.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { AuthGuard } from './../core/guards/auth.guard';
import { UserComponent } from './user.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path:'', component: UserComponent, canActivate:[AuthGuard],
    children: [
      {path:'purchases', component: PurchasesComponent},
      {path:'favorites',component: FavoritesComponent},
      {path:'reviews',component:ReviewsComponent},
      {path:'edit/:id', component: EditUserComponent}
    ]
  },
  

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
