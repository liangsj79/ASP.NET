using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    { 
        Task<IEnumerable<Movie>> Get30HighestGrossingMovies();

        Task<IEnumerable<Movie>> GetMoviesWithReviews();

        Task<IEnumerable<Movie>> GetMoviesByGenreId(int id);

        

    }
}
