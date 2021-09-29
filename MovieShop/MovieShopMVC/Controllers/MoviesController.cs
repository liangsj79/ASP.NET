using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Models;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Details(int id = 1)
        {

            var movie = _movieService.GetMovieDetailById(id);

            return View(movie);
        }

        public IActionResult Genre(int id =1 ,int page =1)
        {
            var movies = _movieService.GetMoviesByGenreId(id);
            var moviesView = new MovieViewModel
            {
                MoviePerPage = 30,
                GenreId = id,
                Movies = movies,
                CurrentPage = page
            };
            return View(moviesView);
        }

       /* public IActionResult GetTopRevenueMovies()
        {
            *//*var movieService = new MovieService();*//*
            var movies = _movieService.Get30HighestGrossingMovies();
            return View(movies);
        }
        // Partial views so that */
    }
}
