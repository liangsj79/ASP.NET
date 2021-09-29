using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieViewModel
    {
        public IEnumerable<MovieCardResponseModel> Movies { get; set; }

        public int GenreId { get; set; }
        public int MoviePerPage { get; set; }
        public int CurrentPage { get; set; }
        
        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Movies.Count() / (double)MoviePerPage));
        }

        public IEnumerable<MovieCardResponseModel> PaginatedMovies()
        {
            int start = (CurrentPage - 1) * MoviePerPage;
            return Movies.OrderBy(m => m.Id).Skip(start).Take(MoviePerPage);
        }
    }
}
