type GenreType = { id: string, name: string };

export default interface MovieDetails {
  title: string;
  poster_path: string;
  release_date: string;
  genres: GenreType[];
  homepage: string;
};