using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository: EfRepository<Cast>,ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }

        public override async Task<Cast> GetByIdAsync(int id)
        {
            var cast = await _dbContext.Casts.Include(c => c.Movies).ThenInclude(mc => mc.Movie).ThenInclude(m => m.Reviews).FirstOrDefaultAsync();
            return cast;
        }

    }
}
