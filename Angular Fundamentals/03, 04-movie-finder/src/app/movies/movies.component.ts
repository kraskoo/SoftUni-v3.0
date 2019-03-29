import { Component, OnInit } from '@angular/core';
import { MoviesService } from '../services/movies.service';
import Movie from '../models/Movie';
import { Router } from '@angular/router';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  popularMovies: Movie[];
  theaterMovies: Movie[];
  kidsPopularMovies: Movie[];
  bestDramasMovies: Movie[];

  constructor(
    private router: Router,
    private moviesService: MoviesService) {}

  ngOnInit() {
    this.moviesService.getPopularMovies().subscribe(data => {
      this.popularMovies = data;
    });
    this.moviesService.getTheaterMovies().subscribe(data => {
      this.theaterMovies = data;
    });
    this.moviesService.getKidsPopularMovies().subscribe(data => {
      this.kidsPopularMovies = data;
    });
    this.moviesService.getbestDramaMovies().subscribe(data => {
      this.bestDramasMovies = data;
    });
  }

  search({ search }) {
    this.router.navigateByUrl(`/movies/search/${search}`);
  }
}