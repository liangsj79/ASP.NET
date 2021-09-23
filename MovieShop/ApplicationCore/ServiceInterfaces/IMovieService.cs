using System;
using System.Collections.Generic;
using ApplicationCore.Models;
namespace ApplicationCore.ServiceInterfaces
{
    //Models
    public interface IMovieService
    {
        IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies(); 
    }
}
