using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

       
        public IActionResult Privacy()
        {
            return View();
        } 
        
        public IActionResult Index()
        {
            // it will look inside views folder =>  folder name with same nsame
            // as COntroller name and look for view with same name as action metjhod name 
            // i want to display top revenue movies
            // get model data

            // models.
            var movies = _movieService.Get30HighestGrossingMovies();
            return View(movies);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
