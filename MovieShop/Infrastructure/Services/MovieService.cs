using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        public async Task<IEnumerable<MovieCardResponseModel>> Get30HighestGrossingMovies()
        {
            var movies = await _movieRepository.Get30HighestGrossingMovies();
        
            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            // mapping entities to models data so that services always return models not entities
            foreach (var movie in movies)
            {
                moviesCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });
            }

            return moviesCardResponseModel;
        }

   

        public async Task<MovieDetailsResponseModel> GetMovieDetailById(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget.GetValueOrDefault(),
                Revenue = movie.Revenue.GetValueOrDefault(),
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                RunTime = movie.RunTime.GetValueOrDefault(),
                Price = movie.Price.GetValueOrDefault()
            };

            movieDetails.Rating = movie.Reviews.Count !=0 ? movie.Reviews.Average(r => r.Rating) : 0.0m;
            movieDetails.Genres = new List<GenreModel>();
            foreach (var movieGenre in movie.Genres)
            {
                
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = movieGenre.Genre.Id,
                    Name = movieGenre.Genre.Name
                });

            }
            

            movieDetails.Casts = new List<CastModel>();
            foreach (var movieCast in movie.Casts)
            {
                
                movieDetails.Casts.Add(new CastModel
                {
                    Id = movieCast.Cast.Id,
                    Name = movieCast.Cast.Name,
                    Character = movieCast.Character,
                    Gender = movieCast.Cast.Gender,
                    ProfilePath = movieCast.Cast.ProfilePath,
                    TmdbUrl = movieCast.Cast.TmdbUrl
                });

            }

            movieDetails.Trailers = new List<TrailerModel>();
            foreach (var trailer in movie.Trailers) {
                
                movieDetails.Trailers.Add(new TrailerModel
                {
                    Id = trailer.Id,
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl,
                    MovieId = trailer.MovieId
                });
            }
               

            return movieDetails;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenreId(int id)
        {
            var movies = await _movieRepository.GetMoviesByGenreId(id);
            var movieCardResponseModel = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });
            }
            return movieCardResponseModel;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetMoviesWithReviews();
            var moviesWithRating = new List<MovieWithRatingModel>();
            foreach (var movie in movies)
            {
                moviesWithRating.Add(new MovieWithRatingModel { movie =  new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl }, rating = movie.Reviews.Count != 0 ? movie.Reviews.Average(r => r.Rating) : 0.0m } );
            }

            var movieCardResponseModel  = moviesWithRating.OrderByDescending(mr => mr.rating).Take(30).Select(mr => mr.movie).ToList();
            return movieCardResponseModel;

        }
    }
}
