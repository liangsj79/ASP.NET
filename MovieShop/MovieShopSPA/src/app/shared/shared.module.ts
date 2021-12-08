import { RouterModule } from '@angular/router';
import { MovieCardComponent } from './component/movie-card/movie-card.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [MovieCardComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    MovieCardComponent
  ],
})
export class SharedModule { }
