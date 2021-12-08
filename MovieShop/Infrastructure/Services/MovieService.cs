using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Serilog;
namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieGenreRepository _movieGenreRepository;

        public MovieService(IMovieRepository movieRepository, IReviewRepository reviewRepository, IMovieGenreRepository movieGenreRepository)
        {
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
            _movieGenreRepository = movieGenreRepository;
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
            Log.Logger =  new LoggerConfiguration().WriteTo.Console().CreateLogger();

            Log.Information("retrieve data！");
            return moviesCardResponseModel;
            
        }

   

        public async Task<MovieDetailsResponseModel> GetMovieDetailsById(int id)
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
            var movieGenres = await _movieGenreRepository.ListAsync(mg => mg.GenreId == id);
            var movieCardResponseModel = new List<MovieCardResponseModel>();
            foreach (var movieGenre in movieGenres)
            {
                movieCardResponseModel.Add(new MovieCardResponseModel { 
                    Id= movieGenre.Movie.Id,
                    PosterUrl = movieGenre.Movie.PosterUrl,
                    Revenue = movieGenre.Movie.Revenue.GetValueOrDefault(),
                    Title = movieGenre.Movie.Title
                });
            }

            return movieCardResponseModel;
           /* var movies = await _movieRepository.GetMoviesByGenreId(id);
            var movieCardResponseModel = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });
            }
            return movieCardResponseModel;*/
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


        public async Task<IEnumerable<ReviewResponseModel>> GetReviewsByMovieId(int id)
        {
            var reviews = await _reviewRepository.ListAsync(p => p.MovieId == id);
            var reviewResponseModel = new List<ReviewResponseModel>();
            foreach ( var review in reviews)
            {
                reviewResponseModel.Add(new ReviewResponseModel
                {
                    ReviewerName = review.User.FirstName + " " + review.User.LastName,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            return reviewResponseModel;
        }
    }
}
