﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MovieShopDbContext:DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options):base(options)
        {

        }

        // have all the dbsets as properties with entity types.

        public DbSet<Genre> Genres { get; set; }
    }
}
