import { Component, Input } from '@angular/core';
import Movie from '../models/Movie';

@Component({
  selector: 'app-movie-info',
  templateUrl: './movie-info.component.html',
  styleUrls: ['./movie-info.component.css']
})
export class MovieInfoComponent {
  @Input('movie-info') movie: Movie;
}