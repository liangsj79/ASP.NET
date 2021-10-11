import { ReviewsComponent } from './user/reviews/reviews.component';
import { FavoritesComponent } from './user/favorites/favorites.component';
import { PurchasesComponent } from './user/purchases/purchases.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { HomeComponent } from './home/home.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { MovieDetailsResolver  } from './movies/movie-details//movie-details-resolver';

const routes: Routes = [
  {path:"", component: HomeComponent},
  {path:"account/login",component: LoginComponent},
  {path:"account/register", component:RegisterComponent},
  {path:"movies/:id", component:MovieDetailsComponent, resolve:{movieDetails: MovieDetailsResolver}},
  {path:"user/purchases", component:PurchasesComponent},
  {path:"user/favorites", component:FavoritesComponent},
  {path:"user/reviews",component:ReviewsComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
