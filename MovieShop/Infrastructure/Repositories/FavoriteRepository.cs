using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository:EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Favorite>> ListAsync(Expression<Func<Favorite, bool>> filter)
        {
            var purchases = await _dbContext.Favorites.Include(f => f.Movie).Where(filter).ToListAsync();
            return purchases;
        }
    }
}
