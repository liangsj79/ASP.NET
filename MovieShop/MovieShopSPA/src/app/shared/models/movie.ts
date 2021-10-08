export interface Movie {
  id: number;
  title: string;
  posterUrl: string;
  backdropUrl: string;
  rating: number;
  overview: string;
  tagline: string;
  budget: number;
  revenue: number;
  imdbUrl: string;
  tmdbUrl: string;
  releaseDate: string;
  runTime: number;
  price: number;
  genres: Genre[];
  casts: Cast[];
  trailers: Trailer[];
}

interface Trailer {
  id: number;
  movieId: number;
  trailerUrl: string;
  name: string;
}

interface Cast {
  id: number;
  name: string;
  gender: string;
  tmdbUrl: string;
  profilePath: string;
  character: string;
}

interface Genre {
  id: number;
  name: string;
}