using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository
    {
        // select top 30 * from Movie order by Revenue
        IEnumerable<Movie> Get30HighestGrossingMovies();
        // select * from Genre
        IEnumerable<Genre> GetAllGenres();

        Movie GetMovieById(int id);

        IEnumerable<Genre> GetGenresById(int id);

        IEnumerable<Trailer> GetTrailersById(int id);

        IEnumerable<CastResponseModel> GetCastsById(int id);
        decimal GetRatingByMovieId(int id);

        IEnumerable<Movie> GetMoviesByGenreId(int id);

    }
}
