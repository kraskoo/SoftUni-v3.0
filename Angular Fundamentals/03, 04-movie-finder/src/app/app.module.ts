import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MoviesComponent } from './movies/movies.component';
import { MoviesService } from './services/movies.service';
import { MovieInfoComponent } from './movie-info/movie-info.component';
import { NavigationComponent } from './navigation/navigation.component';
import { FooterComponent } from './footer/footer.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { SearchMoviesComponent } from './search-movies/search-movies.component';
import MovieDetailsResolver from './services/resolvers/movie-details.resolver';
import SearchMoviesResolver from './services/resolvers/search-movies.resolver';

@NgModule({
  declarations: [
    AppComponent,
    MoviesComponent,
    MovieInfoComponent,
    NavigationComponent,
    FooterComponent,
    MovieDetailsComponent,
    SearchMoviesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    MoviesService,
    MovieDetailsResolver,
    SearchMoviesResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}