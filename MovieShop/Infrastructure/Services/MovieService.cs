using System;
using System.Collections.Generic;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public IEnumerable<MovieCardResponseModel> Get30HighestiGrossingMovies()
        {
            var repo = new MovieRepository();

            //list of movie entities
            var movies = repo.Get30HighestGrossingMovies();
            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            // mapping entities to models data so that services always return models not entities
            foreach (var movie in movies)
            {
                moviesCardResponseModel.Add(new MovieCardResponseModel { Id = movie.Id, PosterUrl = movie.PosterUrl });
            }
            // return list of movieresponse models
            return moviesCardResponseModel;
        }
    }
}
