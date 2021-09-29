using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
using ApplicationCore.Models;
namespace ApplicationCore.ServiceInterfaces
{
    //Models

    public interface IMovieService
    {
        IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies();
        MovieDetailModel GetMovieDetailById(int id);

        IEnumerable<MovieCardResponseModel> GetMoviesByGenreId(int id);
    }
}
