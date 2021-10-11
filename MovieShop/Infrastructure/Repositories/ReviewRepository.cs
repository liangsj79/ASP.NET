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
    public class ReviewRepository: EfRepository<Review>, IReviewRepository
    {

        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Review>> ListAsync(Expression<Func<Review, bool>> filter)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.Movie)
                .Include(r => r.User)
                .Where(filter).ToListAsync();
            return reviews;
        }
    }
}
