using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
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


        public IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies()
        {
            var movies = _movieRepository.Get30HighestGrossingMovies();

            //list of movie entities
        
            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            // mapping entities to models data so that services always return models not entities
            foreach (var movie in movies)
            {
                moviesCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });
            }
            // return list of movieresponse models
            return moviesCardResponseModel;
        }

   

        public MovieDetailModel GetMovieDetailById(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            decimal rating = _movieRepository.GetRatingByMovieId(id);
            IEnumerable<Genre> genres = _movieRepository.GetGenresById(id);
            IEnumerable<Trailer> trailers = _movieRepository.GetTrailersById(id);
            IEnumerable<CastResponseModel> casts = _movieRepository.GetCastsById(id);
            IEnumerable<Genre> allgenres = _movieRepository.GetAllGenres();
            var movieDetailModel = new MovieDetailModel();
            movieDetailModel.BackdropUrl = movie.BackdropUrl;
            movieDetailModel.PosterUrl = movie.PosterUrl;
            movieDetailModel.Title = movie.Title;
            movieDetailModel.Tagline = movie.Tagline;
            movieDetailModel.RunTime = movie.RunTime;
            movieDetailModel.ReleaseYear = movie.ReleaseDate.GetValueOrDefault().Year;
            movieDetailModel.Genres = genres;
            movieDetailModel.Rating = Math.Round(rating, 1);
            movieDetailModel.Overview = movie.Overview;
            movieDetailModel.Price = movie.Price;
            movieDetailModel.ReleaseDate = movie.ReleaseDate.GetValueOrDefault().ToString("MMM dd, yyyy");
            movieDetailModel.Revenue = Math.Round(movie.Revenue.GetValueOrDefault(),2).ToString("N");
            movieDetailModel.Budget = Math.Round(movie.Budget.GetValueOrDefault(),2).ToString("N");
            movieDetailModel.ImdbUrl = movie.ImdbUrl;
            movieDetailModel.Trailers = trailers;
            movieDetailModel.Casts = casts;
            movieDetailModel.AllGenres = allgenres;
            return movieDetailModel;
        }

        public IEnumerable<MovieCardResponseModel> GetMoviesByGenreId(int id)
        {
            var movies = _movieRepository.GetMoviesByGenreId(id);
            var movieCardResponseModel = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl});
            }
            return movieCardResponseModel;
        }
    }
}
