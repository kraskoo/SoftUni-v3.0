import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import MovieDetails from 'src/app/models/MovieDetails';
import { MoviesService } from '../movies.service';

@Injectable()
export default class MovieDetailsResolver implements Resolve<MovieDetails> {
  constructor(private movieService: MoviesService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.movieService.getMovie(route.params['id']);
  }
}