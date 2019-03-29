import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import Movie from '../models/Movie';

@Component({
  selector: 'app-search-movies',
  templateUrl: './search-movies.component.html',
  styleUrls: ['./search-movies.component.css']
})
export class SearchMoviesComponent implements OnInit {
  query: string;
  movies: Movie[];

  constructor(
    private route: ActivatedRoute
  ) {
    this.query = this.route.snapshot.params['query'];
  }

  ngOnInit() {
    this.movies = this.route.snapshot.data['movies'];
  }
}