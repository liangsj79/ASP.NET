using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieGenreRepository : EfRepository<MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async override Task<IEnumerable<MovieGenre>> ListAsync(Expression<Func<MovieGenre, bool>> filter)
        {
            var movieGenre = await _dbContext.MovieGenres.Include(mg => mg.Movie).Where(filter).ToListAsync();
            return movieGenre;

        }
    }
}
