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
    public class PurchaseRepository:EfRepository<Purchase>,IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        { 
        }

        public override async Task<IEnumerable<Purchase>> ListAsync(Expression<Func<Purchase, bool>> filter)
        {
            var purchases = await _dbContext.Purchases.Include(p => p.Movie).Where(filter).ToListAsync();
            return purchases;
        }

    }
}
