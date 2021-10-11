using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI.Controllers
{
    // Attribute Routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.Get30HighestGrossingMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            return Ok(movies);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            var movie = await _movieService.GetMovieDetailsById(id);

            return Ok(movie);
        }


        [Route("toprated")]
        [HttpGet]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();
            if (!movies.Any())
            {
                return NotFound("No movies Found");
            }
            return Ok(movies);
        }

        // api/movies/toprevenue
        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> Get30HighestGrossingMovies()
        {
            var movies = await _movieService.Get30HighestGrossingMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            return Ok(movies);
            // Serialization => object to antoher type of object
            // C# to JSON
            // Deserialization to JSON
            // .NET Core 3.1 or less JSON.NET => 3rd party library included
            // System.Text.Json
            // along with data you aslo need to return HTTP status code
        }

        [Route("genre/{genreId}")]
        [HttpGet]

        public async Task<IActionResult> GetMoviesByGenreId(int genreId)
        {
            var reviews = await _movieService.GetMoviesByGenreId(genreId);
            return Ok(reviews);
        }

        
        [Route("{id}/reviews")]
        [HttpGet]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetReviewsByMovieId(id);
            if (!reviews.Any())
            {
                return NotFound("No reviews Found");
            }
            return Ok(reviews);
        }

    }
}
