using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
namespace ApplicationCore.ServiceInterfaces
{
    //Models

    public interface IMovieService
    {
        Task<IEnumerable<MovieCardResponseModel>> Get30HighestGrossingMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies();
        Task<MovieDetailsResponseModel> GetMovieDetailById(int id);

        Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenreId(int id);
    }
}
