import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MoviesComponent } from './movies/movies.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { SearchMoviesComponent } from './search-movies/search-movies.component';
import MovieDetailsResolver from './services/resolvers/movie-details.resolver';
import SearchMoviesResolver from './services/resolvers/search-movies.resolver';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'movies' },
  { path: 'movies', component: MoviesComponent },
  {
    path: 'movie/:id',
    component: MovieDetailsComponent,
    resolve: {
      details: MovieDetailsResolver
    }
  },
  {
    path: 'movies/search/:query',
    component: SearchMoviesComponent,
    resolve: {
      movies: SearchMoviesResolver
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}