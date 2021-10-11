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

        public async Task<IActionResult> Details(int id = 1)
        {

            var movie = await _movieService.GetMovieDetailsById(id);

            return View(movie);
        }

        public async Task<IActionResult> Genre(int id =1 ,int page =1)
        {
            var movies = await _movieService.GetMoviesByGenreId(id);
            var moviesView = new MovieViewModel
            {
                MoviePerPage = 30,
                GenreId = id,
                Movies = movies,
                CurrentPage = page
            };
            return View(moviesView);
        }

        public async Task<IActionResult> TopRated()
        {
            var movies = await _movieService.GetTopRatedMovies();
            return View(movies);
        }
   
    }
}
