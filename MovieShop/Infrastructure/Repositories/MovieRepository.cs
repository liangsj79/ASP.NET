using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;
using Infrastructure.Data;
using ApplicationCore.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        
        
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }


        public async Task<IEnumerable<Movie>> GetMoviesWithReviews()
        {
            var movies = await _dbContext.Movies.Include(m => m.Reviews).ToListAsync();
            return movies;


        }
        public override async Task<Movie> GetByIdAsync(int id)
        {
            var moviedetails = await _dbContext.Movies.Include(m => m.Genres).ThenInclude(mg => mg.Genre)
                .Include(m => m.Casts).ThenInclude(mc => mc.Cast)
                .Include(m => m.Reviews)
               .Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == id);
            if (moviedetails == null) throw new Exception($"No Movie Found for this {id}");
            return moviedetails;
        }




        


        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int id)
        {
            var movies = await
                (from m in _dbContext.Movies
                 join mg in _dbContext.MovieGenres
                 on m.Id equals mg.MovieId
                 where mg.GenreId == id
                 select m).ToListAsync();
            return movies;
        }


    }
}
