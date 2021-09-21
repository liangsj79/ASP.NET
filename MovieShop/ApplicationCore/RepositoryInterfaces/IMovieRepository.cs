using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> Get30HighestGrossingMovies();
        // select top 30 * from Movie order by Revenue
    }
}
