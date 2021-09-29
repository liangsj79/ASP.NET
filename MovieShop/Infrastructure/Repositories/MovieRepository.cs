using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;
using Infrastructure.Data;
using ApplicationCore.Models;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public MovieRepository(MovieShopDbContext dbContext)
        {
            _movieShopDbContext = dbContext;
        }
        public IEnumerable<Movie> Get30HighestGrossingMovies()
        {
            var movies = _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            return movies;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genres = _movieShopDbContext.Genres.OrderBy(g => g.Name).ToList();
            return genres;
        }

        

        public Movie GetMovieById(int id)
        {
            var movie = _movieShopDbContext.Movies.Where(m => m.Id == id).SingleOrDefault();
            return movie;
        }
        
        
        public decimal GetRatingByMovieId(int id)
        {
            decimal rating = 0.0m;
            var reviews = _movieShopDbContext.Reviews.Where(r => r.MovieId == id).ToList();
            if (reviews.Count() != 0)
            {
                rating = reviews.Average(r => r.Rating);
            }

            return rating;
        }
        public IEnumerable<CastResponseModel> GetCastsById(int id)
        {
            var casts = (from c in _movieShopDbContext.Casts
                        join mc in _movieShopDbContext.MovieCasts
                        on c.Id equals mc.CastId
                        where mc.MovieId == id
                        select new CastResponseModel{ Id= c.Id, Name=c.Name, ProfilePath =c.ProfilePath, Character =mc.Character }).ToList();
            return casts;

        }

        public IEnumerable<Genre> GetGenresById(int id)
        {
            var genres = (from g in _movieShopDbContext.Genres
                        join mg in _movieShopDbContext.MovieGenres
                        on g.Id equals mg.GenreId
                        where mg.MovieId == id
                        select g).ToList();
            return genres;
        }
        public IEnumerable<Trailer> GetTrailersById(int id)
        {
            var trailers = _movieShopDbContext.Trailers.Where(t => t.MovieId == id).ToList();
            return trailers;
        }

        public IEnumerable<Movie> GetMoviesByGenreId(int id)
        {
            var movies = (from m in _movieShopDbContext.Movies
                          join mg in _movieShopDbContext.MovieGenres
                          on m.Id equals mg.MovieId
                          where mg.GenreId == id
                          select m).ToList();
            return movies;
        }
    }
}
