import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import MovieDetails from '../models/MovieDetails';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movie: MovieDetails;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.movie = this.route.snapshot.data['details'];
  }
}