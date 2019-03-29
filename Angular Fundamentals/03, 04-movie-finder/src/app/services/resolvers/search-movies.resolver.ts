import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import Movie from 'src/app/models/Movie';
import { MoviesService } from '../movies.service';

@Injectable()
export default class SearchMoviesResolver implements Resolve<Movie[]> {
  constructor(private movieService: MoviesService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.movieService.searchMovie(route.params['query']);
  }
}