import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import Movie from '../models/Movie';
import MovieDetails from '../models/MovieDetails';

const baseUrl = 'https://api.themoviedb.org/3/';
const apiKey = '&api_key=24650522ecafda2bfad3ff0aeba3cd8b';
const apiKeyAlt = '?api_key=24650522ecafda2bfad3ff0aeba3cd8b';
@Injectable({
  providedIn: 'root'
})
export class MoviesService {
  private popularEndpoint = 'discover/movie?sort_by=popularity.desc';
  private theathersEndpoint = 'discover/movie?primary_release_date.gte=2018-07-15&primary_release_date.lte=2019-02-01';
  private kidsPopularEndpoint = 'discover/movie?certification_country=US&certification.lte=G&sort_by=popularity.desc';
  private bestDramasEndpoint = 'discover/movie?with_genres=18&primary_release_year=2019';

  constructor(private http: HttpClient) {}

  getPopularMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(baseUrl + this.popularEndpoint + apiKey).pipe(map(data => data['results']));
  }

  getTheaterMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(baseUrl + this.theathersEndpoint + apiKey).pipe(map(data => data['results']));
  }

  getKidsPopularMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(baseUrl + this.kidsPopularEndpoint + apiKey).pipe(map(data => data['results']));
  }

  getbestDramaMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(baseUrl + this.bestDramasEndpoint + apiKey).pipe(map(data => data['results']));
  }

  getMovie(id: string): Observable<MovieDetails> {
    return this.http.get<MovieDetails>(baseUrl + `movie/${id}` + apiKeyAlt);
  }

  searchMovie(query: string): Observable<Movie[]> {
    return this.http.get<Movie[]>(baseUrl + 'search/movie' + apiKeyAlt + `&query=${query}`).pipe(map(data => data['results']));
  }
}